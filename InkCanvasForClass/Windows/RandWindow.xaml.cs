using Ink_Canvas.Helpers;
using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ink_Canvas
{
    /// <summary>
    /// RandWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RandWindow : Window
    {
        private readonly Settings _settings;
        private bool _isRolling = false; // 防止重复点击
        private CancellationTokenSource _cts;

        // 使用静态 Random 实例以避免短时间内多次实例化导致的种子重复问题
        private static readonly Random _random = new();

        public static int randSeed; // 保留以兼容外部调用（如果存在）

        public bool IsAutoClose = false;
        public int TotalCount = 1;
        public int PeopleCount = 60;
        public List<string> Names = new();

        // 配置参数
        public int RandWaitingTimes = 100;
        public int RandWaitingThreadSleepTime = 5;
        public int RandMaxPeopleOneTime = 10;
        public int RandDoneAutoCloseWaitTime = 2500;

        public RandWindow(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            InitializeSettings();
            AnimationsHelper.ShowWithSlideFromBottomAndFade(this, 0.25);
        }

        public RandWindow(Settings settings, bool isAutoClose)
        {
            InitializeComponent();
            _settings = settings;
            IsAutoClose = isAutoClose;

            // 自动关闭模式下的 UI 初始化
            PeopleControlPane.Opacity = 0.4;
            PeopleControlPane.IsHitTestVisible = false;

            InitializeSettings();

            // 自动开始逻辑
            Loaded += async (s, e) =>
            {
                await Task.Delay(100);
                await StartRandomSelectionAsync();
            };
        }

        private void InitializeSettings()
        {
            BorderBtnHelp.Visibility = _settings.RandSettings.DisplayRandWindowNamesInputBtn == false ? Visibility.Collapsed : Visibility.Visible;
            RandMaxPeopleOneTime = _settings.RandSettings.RandWindowOnceMaxStudents;
            RandDoneAutoCloseWaitTime = (int)(_settings.RandSettings.RandWindowOnceCloseLatency * 1000);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNames();
        }

        /// <summary>
        /// 加载名字和替换规则
        /// </summary>
        private void LoadNames()
        {
            Names.Clear();
            string namesPath = Path.Combine(App.RootPath, "Names.txt");

            if (File.Exists(namesPath))
            {
                // 加载替换规则
                var replacements = new Dictionary<string, string>();
                string replacePath = Path.Combine(App.RootPath, "Replace.txt");
                if (File.Exists(replacePath))
                {
                    var replaceLines = File.ReadAllLines(replacePath);
                    foreach (var line in replaceLines)
                    {
                    var parts = line.Split(["-->"], StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 2)
                        {
                            replacements[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }

                // 读取并处理名字
                var fileNames = File.ReadAllLines(namesPath);
                foreach (var name in fileNames)
                {
                    if (string.IsNullOrWhiteSpace(name)) continue;

                    string finalName = name;
                    // 应用替换规则
                    if (replacements.TryGetValue(finalName, out string replacement))
                    {
                        finalName = replacement;
                    }

                    Names.Add(finalName);
                }

                PeopleCount = Names.Count;
                TextBlockPeopleCount.Text = PeopleCount.ToString();
            }
            else
            {
                PeopleCount = 0;
            }

            // 如果没有名单，默认使用数字 1-60
            if (PeopleCount == 0)
            {
                PeopleCount = 60;
                TextBlockPeopleCount.Text = "点击此处以导入名单";
            }
        }

        private void BorderBtnAdd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isRolling) return;

            // 检查最大限制
            if (RandMaxPeopleOneTime != -1 && TotalCount >= RandMaxPeopleOneTime) return;
            
            TotalCount++;
            UpdateCounterUI();
        }

        private void BorderBtnMinus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isRolling) return;
            if (TotalCount < 2) return;

            TotalCount--;
            UpdateCounterUI();
        }

        private void UpdateCounterUI()
        {
            LabelNumberCount.Text = TotalCount.ToString();
            SymbolIconStart.Glyph = TotalCount == 1 ? "\uE77B" : "\uE716";
            
            // 恢复按钮透明度
            BorderBtnAdd.Opacity = 1;
            BorderBtnMinus.Opacity = 1;
        }

        private async void BorderBtnRand_MouseUp(object sender, MouseButtonEventArgs e)
        {
            await StartRandomSelectionAsync();
        }

        private async Task StartRandomSelectionAsync()
        {
            if (_isRolling) return;
            _isRolling = true;
            
            // 取消之前的任务（如果有）
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            try
            {
                // 重置结果显示
                LabelOutput2.Visibility = Visibility.Collapsed;
                LabelOutput3.Visibility = Visibility.Collapsed;

                // 1. 滚动动画阶段
                for (int i = 0; i < RandWaitingTimes; i++)
                {
                    if (_cts.Token.IsCancellationRequested) break;

                    int randIndex = _random.Next(0, PeopleCount); // 0-based index
                    string displayName = (Names.Count > 0) ? Names[randIndex] : (randIndex + 1).ToString();

                    LabelOutput.Content = displayName;

                    // 使用 Task.Delay 保持 UI 响应，替代 Thread.Sleep
                    await Task.Delay(RandWaitingThreadSleepTime, _cts.Token);
                }

                // 2. 生成最终结果
                List<string> finalSelection = GenerateRandomSelection(TotalCount);

                // 3. 显示结果
                DisplayResults(finalSelection);

                // 4. 自动关闭逻辑
                if (IsAutoClose)
                {
                    await Task.Delay(RandDoneAutoCloseWaitTime, _cts.Token);
                    PeopleControlPane.Opacity = 1;
                    PeopleControlPane.IsHitTestVisible = true;
                    Close();
                }
            }
            catch (TaskCanceledException)
            {
                // 任务被取消，忽略
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"发生错误: {ex.Message}");
            }
            finally
            {
                _isRolling = false;
                _cts?.Dispose();
                _cts = null;
            }
        }

        /// <summary>
        /// 生成不重复的随机选择结果
        /// </summary>
        private List<string> GenerateRandomSelection(int count)
        {
            HashSet<int> selectedIndices = new();
            List<string> result = new();

            for (int i = 0; i < count; i++)
            {
                // 如果已选满所有人，清空记录以允许重复（防止死循环）
                if (selectedIndices.Count >= PeopleCount)
                {
                    selectedIndices.Clear();
                }

                int randIndex;
                do
                {
                    randIndex = _random.Next(0, PeopleCount);
                } while (selectedIndices.Contains(randIndex));

                selectedIndices.Add(randIndex);

                if (Names.Count > 0)
                {
                    result.Add(Names[randIndex]);
                }
                else
                {
                    result.Add((randIndex + 1).ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// 将结果分布显示到多个 Label 上
        /// </summary>
        private void DisplayResults(List<string> outputs)
        {
            string JoinRange(int start, int count) =>
                string.Join(Environment.NewLine, outputs.Skip(start).Take(count));

            int count = outputs.Count;

            if (count <= 5)
            {
                LabelOutput.Content = JoinRange(0, count);
            }
            else if (count <= 10)
            {
                LabelOutput2.Visibility = Visibility.Visible;
                int mid = (count + 1) / 2;
                LabelOutput.Content = JoinRange(0, mid);
                LabelOutput2.Content = JoinRange(mid, count - mid);
            }
            else
            {
                LabelOutput2.Visibility = Visibility.Visible;
                LabelOutput3.Visibility = Visibility.Visible;

                int third = (count + 1) / 3;
                int twoThirds = (count + 1) * 2 / 3;

                LabelOutput.Content = JoinRange(0, third);
                LabelOutput2.Content = JoinRange(third, twoThirds - third);
                LabelOutput3.Content = JoinRange(twoThirds, count - twoThirds);
            }
        }

        private void BorderBtnHelp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new NamesInputWindow().ShowDialog();
            LoadNames(); // 关闭窗口后重新加载名单
        }

        private void BtnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _cts?.Cancel();
            Close();
        }
    }
}
