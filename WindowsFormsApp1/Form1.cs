using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    

    using Iteration;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public partial class Form1 : Form
    {
        public List<TextBox> textBoxs = new List<TextBox>();
        public List<Button> buttons = new List<Button>();
        public List<CheckBox> checkBoxs = new List<CheckBox>();
        public List<Control> interfaceList = new List<Control>();
        public Thread mainThread;
        public Thread progressThread;
        public Thread testThread;
        public Stopwatch timer;
        public List<double> speedList;
        public int progress;
        public int progressLimit;
        public int progressLast;
        public string timeString;
        public StreamWriter stream;
        public bool threadSleeper;
        public Stopwatch stopwatch;
        public List<Tick> tickListMain;
        public Dictionary<ResultClass, Setup> resultPairDic;
        public string throwPath;
        public Random random = new Random();
        public string pathToFile;
        Dictionary<ResultClass, Setup> resultsOld;
        public bool version { get; set; }
        public double levelStepMind { get; set; }
        public double levelLimitMind { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxs.Add(LOCMin_TB); textBoxs.Add(LOCMax_TB); textBoxs.Add(LOCStep_TB);
            textBoxs.Add(LSCMin_TB); textBoxs.Add(LSCMax_TB); textBoxs.Add(LSCStep_TB);
            textBoxs.Add(POCMin_TB); textBoxs.Add(POCMax_TB); textBoxs.Add(POCStep_TB);
            textBoxs.Add(PSCMin_TB); textBoxs.Add(PSCMax_TB); textBoxs.Add(PSCStep_TB);
            textBoxs.Add(TakeprofitMin_TB); textBoxs.Add(TakeprofitMax_TB); textBoxs.Add(TakeprofitStep_TB);
            textBoxs.Add(StartPipstepMin_TB); textBoxs.Add(StartPipstepMax_TB); textBoxs.Add(StartPipstepStep_TB);
            textBoxs.Add(Lot_TB);
            textBoxs.Add(StrategyCount_TB);
            textBoxs.Add(OrdersCount_TB);
            textBoxs.Add(Accurasity_TB);
            textBoxs.Add(CoefficientFirst_TB);
            textBoxs.Add(CoefficientSecond_TB);
            textBoxs.Add(MarjenLimit_TB);
            textBoxs.Add(Balance_TB);
            textBoxs.Add(GraphicPath_TB);
            textBoxs.Add(ResultPath_TB);
            textBoxs.Add(CoefficientLimit_TB);
            textBoxs.Add(LevelLimit_TB);
            textBoxs.Add(TestDroprate_TB);
            textBoxs.Add(TestCount_TB);

            buttons.Add(Calculate_B);
            buttons.Add(GraphicOpen_B);
            buttons.Add(ResultOpen_B);
            buttons.Add(SetupLoad_B);
            buttons.Add(SetupSave_B);
            buttons.Add(Stop_B);
            buttons.Add(Test_B);
            buttons.Add(Frow_B);

            checkBoxs.Add(ClearProfitFilter_CB);
            checkBoxs.Add(LevelFilter_CB);
            checkBoxs.Add(CoefficientFilter_CB);
            checkBoxs.Add(VriantsAll_CB);

            interfaceList = interfaceList.Concat(textBoxs).Concat(buttons).Concat(checkBoxs).ToList();
        }

        public int MathPow(int value, int pow)
        {
            var result = 1;
            for (var i = 0; i < pow; i++)
                result *= value;
            return result;
        }

        public double MathCeiling(double value, int symbols)
        {
            return Math.Ceiling(value * Math.Pow(10, symbols)) / Math.Pow(10, symbols);
        }

        public void Progress()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();

            Invoke((MethodInvoker)delegate ()
            {
                Calculate_B.Text = "--:--:--";
            });

            while (true)
            {
                Thread.Sleep(500);
                var timeSpain = stopwatch.Elapsed;
                var timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpain.Hours, timeSpain.Minutes, timeSpain.Seconds);

                Invoke((MethodInvoker)delegate ()
                {
                    Calculate_B.Text = progress.ToString() + "/" + progressLimit.ToString() + " " + timeString;
                    Progress_PB.Maximum = progressLimit;

                    if (progress > progressLimit)
                    {
                        progressLimit = progress;
                        Progress_PB.Maximum = progressLimit;
                    }

                    try
                    {
                        Progress_PB.Value = progress;
                    }
                    catch
                    {

                    }
                });
            }
        }

        public int GetIterationCount()
        {
            var amin = LOCMin_TB.Text.ToDouble(); var amax = LOCMax_TB.Text.ToDouble(); var astep = LOCStep_TB.Text.ToDouble(); var acount = (amax - amin) / astep + 1;
            var bmin = LSCMin_TB.Text.ToDouble(); var bmax = LSCMax_TB.Text.ToDouble(); var bstep = LSCStep_TB.Text.ToDouble(); var bcount = (bmax - bmin) / bstep + 1;
            var cmin = TakeprofitMin_TB.Text.ToInt(); var cmax = TakeprofitMax_TB.Text.ToInt(); var cstep = TakeprofitStep_TB.Text.ToInt(); var ccount = (cmax - cmin) / cstep + 1;
            var dmin = StartPipstepMin_TB.Text.ToInt(); var dmax = StartPipstepMax_TB.Text.ToInt(); var dstep = StartPipstepStep_TB.Text.ToInt(); var dcount = (dmax - dmin) / dstep + 1;
            var gmin = POCMin_TB.Text.ToDouble(); var gmax = POCMax_TB.Text.ToDouble(); var gstep = POCStep_TB.Text.ToDouble(); var gcount = (gmax - gmin) / gstep + 1;
            var fmin = PSCMin_TB.Text.ToDouble(); var fmax = PSCMax_TB.Text.ToDouble(); var fstep = PSCStep_TB.Text.ToDouble(); var fcount = (fmax - fmin) / fstep + 1;

            if (VriantsAll_CB.Checked)
                return (int)(acount * bcount * ccount * dcount * gcount * fcount) + 1;
            else
                return (int)((acount + bcount + ccount + dcount+ gcount + fcount) * 3);
        }

        public double Function(Setup setup, List<Tick> tickList, double lot)
        {
            var setupNew = setup.Copy();
            setupNew.lot = lot;
            Result[] resultPair;

            if (version)
            {
                var iterationNew = new IterationNew(setupNew.Copy());
                resultPair = iterationNew.SimulateMathod(setupNew.Copy(), tickList.ToArray());
            }
            else
            {
                var iterationNew = new IterationOld(setupNew.Copy());
                resultPair = iterationNew.SimulateMathod(setupNew.Copy(), tickList.ToArray());
            }

            return resultPair.Min(x => x.levelMin);
        }

        public Dictionary<ResultClass, Setup> ReliseLevelNew(Dictionary<ResultClass, Setup> dictionary, List<Tick> tickArrays)
        {
            int t = 0;
            var result = new Dictionary<ResultClass, Setup>();

            for (var i = 0; i < dictionary.Count; i++)
            {
                double corrector;
                double limit;

                if (throwPath == null)
                {
                    corrector = TestDroprate_TB.Text.ToDouble();
                    limit = LevelLimit_TB.Text.ToDouble();
                }
                else
                {
                    corrector = levelStepMind;
                    limit = levelLimitMind;
                }

                double step = dictionary.Values.ElementAt(i).lot;
                var setupNew = dictionary.Values.ElementAt(i).Copy();

                Result[] resultPair;
                ////---------------------------------
                var iteretionList = new List<LevelIteration>();
                var lotList = new List<double>();
                int index = 0;
                double lot = 0.01;

                var iterationLast = new LevelIteration(setupNew, tickArrays, Function, lot);
                t++;

                iteretionList.Add(iterationLast);
                lotList.Add(lot);

                var iterationBest = new LevelIteration(iterationLast.Lot, iterationLast.Level);
                index++;
                while (true)
                {
                    if (iterationLast.Level <= limit && iterationLast.Level >= 0)
                        break;

                    //if (iterationLast.Level >= -limit && iterationLast.Level <= 0)
                    //    break;

                    if (Math.Abs(iterationBest.Level) > Math.Abs(iterationLast.Level))
                        iterationBest = new LevelIteration(iterationLast.Lot, iterationLast.Level);

                    LevelIteration iterationNew;

                    if (iterationLast.Level < 0 && iteretionList.Count > 1)
                    {
                        if (iteretionList[iteretionList.Count - 2].Level > 0)
                        {
                            var iterationBad = iterationLast;
                            var iterationGood = iteretionList[iteretionList.Count - 2];

                            var l1 = iterationBad.Lot;
                            var l2 = iterationGood.Lot;

                            var L1 = iterationBad.Level;
                            var L2 = iterationGood.Level;

                            var DL1 = Math.Abs(limit - L1);
                            var DL2 = Math.Abs(limit - L2);

                            var lotNew = MathCeiling((DL2 * l1 + DL1 * l2) / (DL1 + DL2), 2);

                            iterationNew = new LevelIteration(setupNew.Copy(), tickArrays, Function, lotNew); t++;
                            if (!lotList.Exists(x => x.Compare(lotNew, "==")))
                                lotList.Add(lotNew);

                            //if (lot.Compare(lotNew, "=="))
                            //    break;

                            if (iterationNew.Level > 0)
                                lot = lotNew;
                        }
                        else
                        {
                            var lotNew = MathCeiling(lot + step, 2);

                            iterationNew = new LevelIteration(setupNew.Copy(), tickArrays, Function, lotNew); t++;
                            if (!lotList.Exists(x => x.Compare(lotNew, "==")))
                                lotList.Add(lotNew);

                            if (iterationNew.Level > 0)
                                lot = lotNew;
                            else
                            {
                                step = MathCeiling(step / corrector, 2);

                                if (step < 0.01)
                                    step = 0.01;
                            }
                        }
                    }
                    else if (iterationLast.Level < 0)
                    {
                        step = MathCeiling(step / corrector, 2);

                        if (step < 0.01)
                            step = 0.01;

                        iterationNew = new LevelIteration(setupNew.Copy(), tickArrays, Function, lot + step); t++;
                    }
                    else if (!lotList.Exists(x => x.Compare(lot + step, "==")))
                    {
                        iterationNew = new LevelIteration(setupNew.Copy(), tickArrays, Function, lot + step); t++;
                        if (!lotList.Exists(x => x.Compare(lot + step, "==")))
                            lotList.Add(lot + step);
                        lotList.Add(lot + step);

                        if (iterationNew.Level > 0)
                            lot = MathCeiling(lot + step, 2);
                        else
                        {
                            step = MathCeiling(step / corrector, 2);

                            if (step < 0.01)
                                step = 0.01;
                        }
                    }
                    else if (!lotList.Exists(x => x.Compare(lot, "==")))
                    {
                        iterationNew = new LevelIteration(setupNew.Copy(), tickArrays, Function, lot); t++;

                        if (!lotList.Exists(x => x.Compare(lot, "==")))
                            lotList.Add(lot);

                        if (iterationNew.Level > 0)
                            lot = MathCeiling(lot, 2);
                        else
                        {
                            step = MathCeiling(step / corrector, 2);

                            if (step < 0.01)
                                step = 0.01;
                        }
                    }
                    else
                    {
                        iterationNew = iterationBest;
                        iterationLast = iterationNew;
                        break;
                    }

                    if (iteretionList.Count > 2)
                        if (iteretionList[iteretionList.Count - 3].Lot.Compare(iteretionList[iteretionList.Count - 2].Lot, "==") && iteretionList[iteretionList.Count - 2].Lot.Compare(iteretionList[iteretionList.Count - 1].Lot, "=="))
                        {
                            iterationNew = iterationBest;
                            iterationLast = iterationNew;
                            break;
                        }

                    iterationLast = iterationNew;
                    iteretionList.Add(iterationLast);

                    index++;
                }

                //if (iterationLast.Level > 100 || iterationLast.Level < -100)
                //    index = index;
                iteretionList.Add(iterationLast);
                //---

                setupNew.lot = MathCeiling(iterationLast.Lot, 2);
                if (AlgorithmType_CB.Checked)
                {
                    var iterationNext = new IterationNew(setupNew.Copy());
                    resultPair = iterationNext.SimulateMathod(setupNew.Copy(), tickArrays.ToArray());
                    t++;
                }
                else
                {
                    var iterationNext = new IterationOld(setupNew.Copy());
                    resultPair = iterationNext.SimulateMathod(setupNew.Copy(), tickArrays.ToArray());
                    t++;
                }

                //---------------------------------
                //setupNew.lot = Math.Round(setupNew.lot, 2);

                var startResult1 = resultPair[0];
                var startResult2 = resultPair[1];

                var clearProfit1 = startResult1.profit + startResult1.mass;
                var clearProfit2 = startResult2.profit + startResult2.mass;

                double coefficient;
                if (clearProfit1 == 0 || clearProfit2 == 0)
                    coefficient = 200000d;
                else if (clearProfit1 < 0 && clearProfit2 < 0)
                    coefficient = 200000d;
                else
                    coefficient = Math.Min(Math.Abs(clearProfit1), Math.Abs(clearProfit2)) / Math.Max(Math.Abs(clearProfit1), Math.Abs(clearProfit2));

                var resultTemporary = new ResultClass()
                {
                    levelMin = Math.Min(startResult1.levelMin, startResult2.levelMin),
                    margin = startResult2.margin,
                    marginMax = Math.Max(startResult1.marginMax, startResult2.marginMax),
                    mass = startResult1.mass + startResult2.mass,
                    massMin = Math.Min(startResult1.massMin, startResult2.massMin),
                    profit = startResult1.profit + startResult2.profit,
                    coefficient = coefficient,
                    ordersOpenedCount1 = startResult1.ordersOpenedCount1,
                    ordersClosedCount1 = startResult1.ordersClosedCount1,
                    ordersOpenedCount2 = startResult2.ordersOpenedCount2,
                    ordersClosedCount2 = startResult2.ordersClosedCount2
                };

                result.Add(resultTemporary, setupNew.Copy());
            }

            //MessageBox.Show(t.ToString() + "\n" + result.ElementAt(0).Key.levelMin + "\n" + result.ElementAt(0).Value.lot);

            return result;
        }

        public Dictionary<ResultClass, Setup> ReliseLevelOld(Dictionary<ResultClass, Setup> dictionary, List<Tick> tickArrays)
        {
            int t = 0;
            var result = new Dictionary<ResultClass, Setup>();

            for (var i = 0; i < dictionary.Count; i++)
            {
                double step = Lot_TB.Text.ToDouble();
                double lotLast = Lot_TB.Text.ToDouble(); ;
                double corrector = TestDroprate_TB.Text.ToDouble();
                var limit = LevelLimit_TB.Text.ToDouble();

                var setupNew = dictionary.Values.ElementAt(i).Copy();

                Result[] resultPair;

                while (true)
                {
                    while (threadSleeper)
                        Thread.Sleep(5000);

                    step = Math.Round(step, 2);
                    lotLast = Math.Round(lotLast, 2);
                    setupNew.lot = Math.Round(setupNew.lot, 2);

                    if (AlgorithmType_CB.Checked)
                    {
                        var iterationNew = new IterationNew(setupNew.Copy());
                        resultPair = iterationNew.SimulateMathod(setupNew.Copy(), tickListMain.ToArray()); t++;
                    }
                    else
                    {
                        var iterationNew = new IterationOld(setupNew.Copy());
                        resultPair = iterationNew.SimulateMathod(setupNew.Copy(), tickListMain.ToArray()); t++;
                    }

                    var lowerLimitPlus = resultPair[0].levelMin.Compare(limit, "<=") || resultPair[1].levelMin.Compare(limit, "<=");
                    var hierLimitPlus = resultPair[0].levelMin.Compare(limit, ">") || resultPair[1].levelMin.Compare(limit, ">");
                    var lowerLimitMinus = resultPair[0].levelMin.Compare(0, "<") || resultPair[1].levelMin.Compare(0, "<");
                    var hierLimitMinus = resultPair[0].levelMin.Compare(0, ">=") || resultPair[1].levelMin.Compare(0, ">=");
                    var inField = lowerLimitPlus && hierLimitMinus;

                    if (inField)
                        break;
                    else if (hierLimitPlus)
                    {
                        if (Math.Round(setupNew.lot + step, 2).Compare(Math.Round(lotLast, 2), "==") && Math.Round(step, 2).Compare(0.01, ">"))
                            step = step * corrector;

                        if (Math.Round(setupNew.lot + step, 2).Compare(Math.Round(lotLast, 2), "=="))
                            break;

                        setupNew.lot = setupNew.lot + step;
                        lotLast = setupNew.lot - step;

                        if (Math.Round(step).Compare(0.0, "=="))
                            step = 0.01;
                    }
                    else if (lowerLimitMinus)
                    {
                        if (Math.Round(setupNew.lot - step, 2).Compare(0.01, ">="))
                        {
                            if (Math.Round(setupNew.lot - step, 2).Compare(Math.Round(lotLast, 2), "==") && Math.Round(step, 2).Compare(0.01, ">"))
                                step = step * corrector;

                            setupNew.lot = setupNew.lot - step;
                            lotLast = setupNew.lot + step;
                        }
                        else
                            while (Math.Round(setupNew.lot - step, 2).Compare(0.01, "<") && Math.Round(step, 2).Compare(0.01, ">"))
                                step = step * corrector;

                        step = Math.Round(step, 2);
                        lotLast = Math.Round(lotLast, 2);
                        setupNew.lot = Math.Round(setupNew.lot, 2);

                        if (Math.Round(setupNew.lot - step, 2).Compare(0.01, ">=") && Math.Round(step, 2).Compare(0.01, ">"))
                        {
                            if (Math.Round(setupNew.lot - step, 2).Compare(Math.Round(lotLast, 2), "==") && Math.Round(step, 2).Compare(0.01, ">"))
                                step = step * corrector;

                            setupNew.lot = setupNew.lot - step;
                            lotLast = setupNew.lot + step;
                        }
                        else if (Math.Round(setupNew.lot, 2).Compare(0.01, "==") && Math.Round(step, 2).Compare(0.01, "=="))
                        {
                            if (AlgorithmType_CB.Checked)
                            {
                                var iterationNew = new IterationNew(setupNew.Copy());
                                resultPair = iterationNew.SimulateMathod(setupNew.Copy(), tickArrays.ToArray()); t++;
                            }
                            else
                            {
                                var iterationNew = new IterationOld(setupNew.Copy());
                                resultPair = iterationNew.SimulateMathod(setupNew.Copy(), tickArrays.ToArray()); t++;
                            }

                            break;
                        }

                        if (Math.Round(step).Compare(0.0, "=="))
                            step = 0.01;
                    }
                }

                setupNew.lot = Math.Round(setupNew.lot, 2);

                var startResult1 = resultPair[0];
                var startResult2 = resultPair[1];

                var clearProfit1 = startResult1.profit + startResult1.mass;
                var clearProfit2 = startResult2.profit + startResult2.mass;

                double coefficient;
                if (clearProfit1 == 0 || clearProfit2 == 0)
                    coefficient = 200000d;
                else if (clearProfit1 < 0 && clearProfit2 < 0)
                    coefficient = 200000d;
                else
                    coefficient = Math.Min(Math.Abs(clearProfit1), Math.Abs(clearProfit2)) / Math.Max(Math.Abs(clearProfit1), Math.Abs(clearProfit2));

                var resultTemporary = new ResultClass()
                {
                    levelMin = Math.Min(startResult1.levelMin, startResult2.levelMin),
                    margin = startResult2.margin,
                    marginMax = Math.Max(startResult1.marginMax, startResult2.marginMax),
                    mass = startResult1.mass + startResult2.mass,
                    massMin = Math.Min(startResult1.massMin, startResult2.massMin),
                    profit = startResult1.profit + startResult2.profit,
                    coefficient = coefficient,
                    ordersOpenedCount1 = startResult1.ordersOpenedCount1,
                    ordersClosedCount1 = startResult1.ordersClosedCount1,
                    ordersOpenedCount2 = startResult2.ordersOpenedCount2,
                    ordersClosedCount2 = startResult2.ordersClosedCount2
                };

                result.Add(resultTemporary, setupNew.Copy());
            }

            //MessageBox.Show(t.ToString() + "\n" + result.ElementAt(0).Key.levelMin + "\n" + result.ElementAt(0).Value.lot);
            return result;
        }

        public Dictionary<ResultClass, Setup> SearchOptimum(double startCoefficient, Setup setupStart, ResultClass result, double[] minArray, double[] maxArray, double[] stepArray)
        {
            var resultDic = new Dictionary<ResultClass, Setup>();
            var coefficientUpLast = startCoefficient;
            var coefficientDownLast = startCoefficient;

            var setupUpNew = new double[] { setupStart.LOC, setupStart.LSC, setupStart.TP, setupStart.startPipstep, setupStart.POC, setupStart.PSC };
            var setupDownNew = new double[] { setupStart.LOC, setupStart.LSC, setupStart.TP, setupStart.startPipstep, setupStart.POC, setupStart.PSC };

            result.coefficient = startCoefficient;

            for (var type = 0; type < minArray.Length; type++)
            {
                for (var value = (double)Math.Round(minArray[type] + stepArray[type], 2); (double)Math.Round(value, 2) <= (double)Math.Round(maxArray[type], 2); value = (double)Math.Round(value + stepArray[type], 2))
                {
                    setupUpNew[type] = Math.Round(value, 2);

                    var setupTemporary = setupStart.Copy();
                    setupTemporary.LOC = setupUpNew[0];
                    setupTemporary.LSC = setupUpNew[1];
                    setupTemporary.TP = (int)Math.Round(setupUpNew[2], 0);
                    setupTemporary.startPipstep = (int)Math.Round(setupUpNew[3], 0);
                    setupTemporary.POC = setupUpNew[4];
                    setupTemporary.PSC = setupUpNew[5];

                    Result[] pair;
                    if (AlgorithmType_CB.Checked)
                    {
                        var iterationNew = new IterationNew(setupTemporary.Copy());
                        try
                        {
                            pair = iterationNew.SimulateMathod(setupTemporary.Copy(), tickListMain.ToArray());
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        var iterationNew = new IterationOld(setupTemporary.Copy());
                        try
                        {
                            pair = iterationNew.SimulateMathod(setupTemporary.Copy(), tickListMain.ToArray());
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    var result1 = new ResultClass(pair[0]);
                    var result2 = new ResultClass(pair[1]);

                    result = new ResultClass()
                    {
                        levelMin = Math.Min(result1.levelMin, result2.levelMin),
                        margin = result2.margin,
                        marginMax = Math.Max(result1.marginMax, result2.marginMax),
                        mass = result1.mass + result2.mass,
                        massMin = Math.Min(result1.massMin, result2.massMin),
                        profit = result1.profit + result2.profit,
                        ordersOpenedCount1 = result1.ordersOpenedCount1,
                        ordersClosedCount1 = result1.ordersClosedCount1,
                        ordersOpenedCount2 = result2.ordersOpenedCount2,
                        ordersClosedCount2 = result2.ordersClosedCount2
                    };

                    var clearProfit1 = result1.profit + result1.mass;
                    var clearProfit2 = result2.profit + result2.mass;

                    double coefficientNew;
                    if (clearProfit1 == 0 || clearProfit2 == 0)
                        coefficientNew = 200000d;
                    else if (clearProfit1 < 0 && clearProfit2 < 0)
                        coefficientNew = 200000d;
                    else
                        coefficientNew = Math.Min(Math.Abs(clearProfit1), Math.Abs(clearProfit2)) / Math.Max(Math.Abs(clearProfit1), Math.Abs(clearProfit2));
                    result.coefficient = coefficientNew;


                    var resultNew = new Dictionary<ResultClass, Setup>();
                    resultNew.Add(result, setupTemporary.Copy());

                    if (LevelFilter_CB.Checked)
                    {
                        if (LevelType_RB.Checked)
                            resultNew = ReliseLevelOld(resultNew, tickListMain);
                        else
                            resultNew = ReliseLevelNew(resultNew, tickListMain);
                    }

                    resultDic.Add(resultNew.ElementAt(0).Key, resultNew.ElementAt(0).Value);

                    coefficientNew = resultNew.ElementAt(0).Key.coefficient;

                    if (Math.Abs(1 - coefficientUpLast) > Math.Abs(1 - coefficientNew))
                        coefficientUpLast = coefficientNew;
                    else
                        break;
                }

                for (var value = (double)Math.Round(maxArray[type] - stepArray[type], type); (double)Math.Round(value, 2) >= (double)Math.Round(minArray[type], 2); value = (double)Math.Round(value - stepArray[type], 2))
                {
                    setupDownNew[type] = Math.Round(value, 2);

                    var setupTemporary = setupStart.Copy();
                    setupTemporary.LOC = setupDownNew[0];
                    setupTemporary.LSC = setupDownNew[1];
                    setupTemporary.TP = (int)Math.Round(setupDownNew[2], 0);
                    setupTemporary.startPipstep = (int)Math.Round(setupDownNew[3], 0);
                    setupTemporary.POC = setupDownNew[4];
                    setupTemporary.PSC = setupDownNew[5];

                    Result[] pair;
                    if (AlgorithmType_CB.Checked)
                    {
                        var iterationNew = new IterationNew(setupTemporary.Copy());
                        try
                        {
                            pair = iterationNew.SimulateMathod(setupTemporary.Copy(), tickListMain.ToArray());
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    else
                    {
                        var iterationNew = new IterationOld(setupTemporary.Copy());
                        try
                        {
                            pair = iterationNew.SimulateMathod(setupTemporary.Copy(), tickListMain.ToArray());
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    var result1 = new ResultClass(pair[0]);
                    var result2 = new ResultClass(pair[1]);

                    result = new ResultClass()
                    {
                        levelMin = Math.Min(result1.levelMin, result2.levelMin),
                        margin = result2.margin,
                        marginMax = Math.Max(result1.marginMax, result2.marginMax),
                        mass = result1.mass + result2.mass,
                        massMin = Math.Min(result1.massMin, result2.massMin),
                        profit = result1.profit + result2.profit,
                        ordersOpenedCount1 = result1.ordersOpenedCount1,
                        ordersClosedCount1 = result1.ordersClosedCount1,
                        ordersOpenedCount2 = result2.ordersOpenedCount2,
                        ordersClosedCount2 = result2.ordersClosedCount2
                    };

                    var clearProfit1 = result1.profit + result1.mass;
                    var clearProfit2 = result2.profit + result2.mass;

                    double coefficientNew;
                    if (clearProfit1 == 0 || clearProfit2 == 0)
                        coefficientNew = 200000d;
                    else if (clearProfit1 < 0 && clearProfit2 < 0)
                        coefficientNew = 200000d;
                    else
                        coefficientNew = Math.Min(Math.Abs(clearProfit1), Math.Abs(clearProfit2)) / Math.Max(Math.Abs(clearProfit1), Math.Abs(clearProfit2)); ;
                    result.coefficient = coefficientNew;

                    var resultNew = new Dictionary<ResultClass, Setup>();
                    resultNew.Add(result, setupTemporary);

                    if (LevelFilter_CB.Checked)
                    {
                        if (LevelType_RB.Checked)
                            resultNew = ReliseLevelOld(resultNew, tickListMain);
                        else
                            resultNew = ReliseLevelNew(resultNew, tickListMain);
                    }

                    resultDic.Add(resultNew.ElementAt(0).Key, resultNew.ElementAt(0).Value);

                    coefficientNew = resultNew.ElementAt(0).Key.coefficient;

                    if (Math.Abs(1 - coefficientDownLast) > Math.Abs(1 - coefficientNew))
                        coefficientDownLast = coefficientNew;
                    else
                        break;
                }
            }

            return resultDic;
        }

        public StreamWriter AgregateStart(string name)
        {
            var id = "";
            foreach (var tb in textBoxs)
                id += tb.Text + "\t";

            foreach (var cb in checkBoxs)
                id += cb.Checked + "\t";

            StreamWriter stream;
            string algorithm;
            string type;

            if (AlgorithmType_CB.Checked)
                algorithm = "2.00";
            else
                algorithm = "1.72";

            if (VriantsAll_CB.Checked && throwPath == null)
                type = "E";
            else if (throwPath == null)
                type = "T";
            else
                type = "F";

            var path = ResultPath_TB.Text + "\\" + name + "_" + algorithm + "_" + type + "-" + StrategyCount_TB.Text + "_" + random.Next(999999999) + ".txt";

            pathToFile = path;

            stream = new StreamWriter(path);
            stream.WriteLine(id);

            if (throwPath == null)
            {
                stream.WriteLine(String.Format("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}",
                    "MM, c",
                    "mm, c",
                    "Mass, c",
                    "Profit, c",
                    "Clear Profit, c",
                    "Level, %",
                    "Coefficient",
                    "Op. Ord. 1",
                    "Cl. Ord. 1",
                    "Op. Ord. 2",
                    "Cl. Ord. 2",
                    "Lot",
                    "LOC",
                    "LSC",
                    "POC",
                    "PSC",
                    "TP",
                    "Start Pipstep",
                    "First Coefficient",
                    "Second Coefficient",
                    "Strat. Count",
                    "Orders Count",
                    "Balance",
                    "Accurasity"
                    ));
            }
            else
            {
                stream.WriteLine(String.Format("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}\t{24}\t{25}\t{26}",
                    "MM, c",
                    "mm, c",
                    "Mass, c",
                    "Profit, c",
                    "Clear Profit, c",
                    "Level, %",
                    "Coefficient",
                    "Op. Ord. 1",
                    "Cl. Ord. 1",
                    "Op. Ord. 2",
                    "Cl. Ord. 2",
                    "Lot",
                    "LOC",
                    "LSC",
                    "POC",
                    "PSC",
                    "TP",
                    "Start Pipstep",
                    "First Coefficient",
                    "Second Coefficient",
                    "Strat. Count",
                    "Orders Count",
                    "Balance",
                    "Accurasity",
                    "Clear Profit Deltha",
                    "Coefficient Deltha",
                    "Level Deltha"
                    ));
            }

            return stream;
        }

        public void AgregateRecord(Dictionary<ResultClass, Setup> resultDictionary, StreamWriter stream)
        {
            double clearProfitLimit = 0;
            double coefficientLimit = 0;

            Invoke((MethodInvoker)delegate ()
            {
                clearProfitLimit = MarjenLimit_TB.Text.ToDouble();
                coefficientLimit = CoefficientLimit_TB.Text.ToDouble();
            });

            var topFiltered = resultDictionary.OrderBy(x => x.Value.HashCode).ToList();

            if (ClearProfitFilter_CB.Checked)
                topFiltered = topFiltered.Where(x => x.Key.profit + x.Key.mass >= clearProfitLimit).ToList();

            if (CoefficientFilter_CB.Checked)
                topFiltered = topFiltered.Where(x => x.Key.coefficient <= coefficientLimit).ToList();

            if (LevelFilter_CB.Checked)
                topFiltered = topFiltered.Where(x => x.Key.levelMin <= LevelLimit_TB.Text.ToDouble() && x.Key.levelMin >= 0).ToList();

            var setupList = topFiltered.Select(x => x.Value).ToList();

            var variantsBefore2 = topFiltered.Count();

            if (topFiltered.Count <= 0)
                return;

            var top = topFiltered.ToList();

            var resultList = new List<string>();

            for (var i = 0; i < top.Count; i++)
                if (top[i].Key.marginMax != 0)
                {
                    string resultString;

                    if (throwPath == null)
                        resultString = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
                        Math.Round(top[i].Key.marginMax, 4).ToString(),
                        Math.Round(top[i].Key.massMin, 4).ToString(),
                        Math.Round(top[i].Key.mass, 4).ToString(),
                        Math.Round(top[i].Key.profit, 4).ToString(),
                        Math.Round((top[i].Key.profit + top[i].Key.mass), 4).ToString(),
                        Math.Round(top[i].Key.levelMin, 4).ToString(),
                        Math.Round(top[i].Key.coefficient, 4).ToString(),
                        top[i].Key.ordersOpenedCount1.ToString(),
                        top[i].Key.ordersClosedCount1.ToString(),
                        top[i].Key.ordersOpenedCount2.ToString(),
                        top[i].Key.ordersClosedCount2.ToString(),
                        top[i].Value.ToString()
                        );
                    else
                        resultString = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}",
                        Math.Round(top[i].Key.marginMax, 4).ToString(),
                        Math.Round(top[i].Key.massMin, 4).ToString(),
                        Math.Round(top[i].Key.mass, 4).ToString(),
                        Math.Round(top[i].Key.profit, 4).ToString(),
                        Math.Round((top[i].Key.profit + top[i].Key.mass), 4).ToString(),
                        Math.Round(top[i].Key.levelMin, 4).ToString(),
                        Math.Round(top[i].Key.coefficient, 4).ToString(),
                        top[i].Key.ordersOpenedCount1.ToString(),
                        top[i].Key.ordersClosedCount1.ToString(),
                        top[i].Key.ordersOpenedCount2.ToString(),
                        top[i].Key.ordersClosedCount2.ToString(),
                        top[i].Value.ToString(),
                        top[i].Key.profit + top[i].Key.mass - (resultsOld.Keys.ElementAt(i).profit + resultsOld.Keys.ElementAt(i).mass),
                        top[i].Key.coefficient - resultsOld.Keys.ElementAt(i).coefficient,
                        top[i].Key.levelMin - resultsOld.Keys.ElementAt(i).levelMin
                        );

                    if (!resultList.Contains(resultString))
                        stream.WriteLine(resultString);
                }
        }

        public object LoadRealGraphic(FileInfo file)
        {
            List<string> resultList;

            StreamReader readingStrim;
            try { readingStrim = new StreamReader(file.FullName); resultList = new List<string>(); }
            catch { MessageBox.Show("Файл недоступен для чтения. Запустите программу от с правами администратора и повторите попытку."); return false; }

            while (!readingStrim.EndOfStream)
                resultList.Add(readingStrim.ReadLine());

            resultList.RemoveAt(0);
            resultList.RemoveAt(resultList.Count - 1);
            readingStrim.Close();

            return resultList;
        }

        private List<Tick> ConvertToTickList(List<string> inputList, double itemRevers)
        {
            List<Tick> resultList = null;

            try
            {
                resultList = new List<Tick>();
                foreach (var element in inputList)
                {
                    var temporaryArray = element.Split(':');
                    var temporaryTick = new Tick
                    {
                        time = (int)Math.Round(temporaryArray[0].ToDouble(), (int)Math.Log10(itemRevers)),
                        sell = (int)Math.Round(temporaryArray[1].ToDouble() * itemRevers, 0),
                        buy = (int)Math.Round(temporaryArray[2].ToDouble() * itemRevers, 0)
                    };

                    resultList.Add(temporaryTick);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при создании списка тиков.");
                return resultList;
            }

            return resultList;
        }

        private void Calculate_B_Click(object sender, EventArgs e)
        {
            throwPath = null;

            mainThread = new Thread(new ThreadStart(Calculate))
            {
                Priority = ThreadPriority.Lowest
            };

            mainThread.Start();

            InterfaceOff();
        }

        private void Frow_B_Click(object sender, EventArgs e)
        {
            throwPath = null;
            tickListMain = null;

            using (var ofd1 = new OpenFileDialog())
            {
                ofd1.Filter = "txt files (*.txt)|*.txt";
                if (ofd1.ShowDialog() == DialogResult.OK)
                    throwPath = ofd1.FileName;
                else
                    return;
            }

            mainThread = new Thread(new ThreadStart(Calculate))
            {
                Priority = ThreadPriority.Lowest
            };

            mainThread.Start();

            InterfaceOff();
        }

        private void Calculate()
        {
            version = AlgorithmType_CB.Checked;

            var file = new FileInfo(GraphicPath_TB.Text);
            InterfaceOff();

            progressThread = new Thread(new ThreadStart(Progress))
            {
                Priority = ThreadPriority.Highest
            };
            progressThread.Start();

            var lotSize = Lot_TB.Text.ToDouble();
            var strategyCount = StrategyCount_TB.Text.ToInt();
            var ordersCount = OrdersCount_TB.Text.ToInt();

            var setups = new List<Setup>();
            resultsOld = new Dictionary<ResultClass, Setup>();
            var itemRevers = MathPow(10, Accurasity_TB.Text.ToInt());
            double coefficientFirst = CoefficientFirst_TB.Text.ToDouble();
            double coefficientSecond = CoefficientSecond_TB.Text.ToDouble();
            double balance = Balance_TB.Text.ToDouble();

            var amin = LOCMin_TB.Text.ToDouble(); var amax = LOCMax_TB.Text.ToDouble(); var astep = LOCStep_TB.Text.ToDouble();
            var bmin = LSCMin_TB.Text.ToDouble(); var bmax = LSCMax_TB.Text.ToDouble(); var bstep = LSCStep_TB.Text.ToDouble();
            var cmin = TakeprofitMin_TB.Text.ToInt(); var cmax = TakeprofitMax_TB.Text.ToInt(); var cstep = TakeprofitStep_TB.Text.ToInt();
            var dmin = StartPipstepMin_TB.Text.ToInt(); var dmax = StartPipstepMax_TB.Text.ToInt(); var dstep = StartPipstepStep_TB.Text.ToInt();
            var gmin = POCMin_TB.Text.ToDouble(); var gmax = POCMax_TB.Text.ToDouble(); var gstep = POCStep_TB.Text.ToDouble();
            var fmin = PSCMin_TB.Text.ToDouble(); var fmax = PSCMax_TB.Text.ToDouble(); var fstep = PSCStep_TB.Text.ToDouble();

            if (VriantsAll_CB.Checked && throwPath == null)
            {
                for (var a = Math.Round(amin, 2); Math.Round(a, 2) <= Math.Round(amax, 2); a += astep)
                    for (var b = Math.Round(bmin, 2); Math.Round(b, 2) <= Math.Round(bmax, 2); b += bstep)
                        for (var c = cmin; c <= cmax; c += cstep)
                            for (var d = dmin; d <= dmax; d += dstep)
                                for (var g = Math.Round(gmin, 2); Math.Round(g, 2) <= Math.Round(gmax, 2); g += gstep)
                                    for (var f = Math.Round(fmin, 2); Math.Round(f, 2) <= Math.Round(fmax, 2); f += fstep)
                                    {
                                        var setup = new Setup
                                        (
                                            lotSize,
                                            (double)a,
                                            (double)b,
                                            c,
                                            strategyCount,
                                            ordersCount,
                                            d,
                                            (double)g,
                                            (double)f,
                                            balance,
                                            itemRevers,
                                            coefficientFirst,
                                            coefficientSecond
                                        );

                                        setups.Add(setup);
                                    }
            }
            else if (throwPath == null)
            {
                for (var a = Math.Round(amin, 2); Math.Round(a, 2) <= Math.Round(amax, 2); a += astep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)a,
                        bmin,
                        cmin,
                        strategyCount,
                        ordersCount,
                        dmin,
                        gmin,
                        fmin,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var a = Math.Round(amax, 2); Math.Round(a, 2) >= Math.Round(amin, 2); a -= astep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)a,
                        bmax,
                        cmax,
                        strategyCount,
                        ordersCount,
                        dmax,
                        gmax,
                        fmax,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var a = Math.Round(amin, 2); Math.Round(a, 2) <= Math.Round(amax, 2); a += astep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)a,
                        (double)Math.Round((bmin + bmax) / 2d, 2),
                        (int)Math.Round((cmin + cmax) / 2d, 0),
                        strategyCount,
                        ordersCount,
                        (int)Math.Round((dmin + dmax) / 2d, 0),
                        (double)Math.Round((gmin + gmax) / 2d, 2),
                        (double)Math.Round((fmin + fmax) / 2d, 2),
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }

                for (var b = Math.Round(bmin, 2); Math.Round(b, 2) <= Math.Round(bmax, 2); b += bstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amin,
                        (double)b,
                        cmin,
                        strategyCount,
                        ordersCount,
                        dmin,
                        gmin,
                        fmin,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var b = Math.Round(bmax, 2); Math.Round(b, 2) >= Math.Round(bmin, 2); b -= bstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amax,
                        (double)b,
                        cmax,
                        strategyCount,
                        ordersCount,
                        dmax,
                        gmax,
                        fmax,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var b = Math.Round(bmin, 2); Math.Round(b, 2) <= Math.Round(bmax, 2); b += bstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)Math.Round((amin + amax) / 2d, 2),
                        (double)b,
                        (int)Math.Round((cmin + cmax) / 2d, 0),
                        strategyCount,
                        ordersCount,
                        (int)Math.Round((dmin + dmax) / 2d, 0),
                        (double)Math.Round((gmin + gmax) / 2d, 2),
                        (double)Math.Round((fmin + fmax) / 2d, 2),
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }

                for (var c = cmin; c <= cmax; c += cstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amin,
                        bmin,
                        c,
                        strategyCount,
                        ordersCount,
                        dmin,
                        gmin,
                        fmin,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var c = cmax; c >= cmin; c -= cstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amax,
                        bmax,
                        c,
                        strategyCount,
                        ordersCount,
                        dmax,
                        gmax,
                        fmax,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var c = cmin; c <= cmax; c += cstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)Math.Round((amin + amax) / 2d, 2),
                        (double)Math.Round((bmin + bmax) / 2d, 2),
                        c,
                        strategyCount,
                        ordersCount,
                        (int)Math.Round((dmin + dmax) / 2d, 0),
                        (double)Math.Round((gmin + gmax) / 2d, 2),
                        (double)Math.Round((fmin + fmax) / 2d, 2),
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }

                for (var d = dmin; d <= dmax; d += dstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amin,
                        bmin,
                        cmin,
                        strategyCount,
                        ordersCount,
                        d,
                        gmin,
                        fmin,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var d = dmax; d >= dmin; d -= dstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amax,
                        bmax,
                        cmax,
                        strategyCount,
                        ordersCount,
                        d,
                        gmax,
                        fmax,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var d = dmin; d <= dmax; d += dstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)Math.Round((amin + amax) / 2d, 2),
                        (double)Math.Round((bmin + bmax) / 2d, 2),
                        (int)Math.Round((cmin + cmax) / 2d, 0),
                        strategyCount,
                        ordersCount,
                        d,
                        (double)Math.Round((gmin + gmax) / 2d, 2),
                        (double)Math.Round((fmin + fmax) / 2d, 2),
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }

                for (var g = Math.Round(gmin, 2); Math.Round(g, 2) <= Math.Round(gmax, 2); g += gstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amin,
                        bmin,
                        cmin,
                        strategyCount,
                        ordersCount,
                        dmin,
                        (double)g,
                        fmin,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var g = Math.Round(gmax, 2); Math.Round(g, 2) >= Math.Round(gmin, 2); g -= gstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amax,
                        bmax,
                        cmax,
                        strategyCount,
                        ordersCount,
                        dmax,
                        (double)g,
                        fmax,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var g = Math.Round(gmin, 2); Math.Round(g, 2) <= Math.Round(gmax, 2); g += gstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)Math.Round((amin + amax) / 2d, 2),
                        (double)Math.Round((bmin + bmax) / 2d, 2),
                        (int)Math.Round((cmin + cmax) / 2d, 0),
                        strategyCount,
                        ordersCount,
                        (int)Math.Round((dmin + dmax) / 2d, 0),
                        (double)g,
                        (double)Math.Round((fmin + fmax) / 2d, 2),
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }

                for (var f = Math.Round(fmin, 2); Math.Round(f, 2) <= Math.Round(fmax, 2); f += fstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amin,
                        bmin,
                        cmin,
                        strategyCount,
                        ordersCount,
                        dmin,
                        gmin,
                        (double)f,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var f = Math.Round(fmax, 2); Math.Round(f, 2) >= Math.Round(fmin, 2); f -= fstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        amax,
                        bmax,
                        cmax,
                        strategyCount,
                        ordersCount,
                        dmax,
                        gmax,
                        (double)f,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
                for (var f = Math.Round(fmin, 2); Math.Round(f, 2) <= Math.Round(fmax, 2); f += fstep)
                {
                    var setup = new Setup
                    (
                        lotSize,
                        (double)Math.Round((amin + amax) / 2d, 2),
                        (double)Math.Round((bmin + bmax) / 2d, 2),
                        (int)Math.Round((cmin + cmax) / 2d, 0),
                        strategyCount,
                        ordersCount,
                        (int)Math.Round((dmin + dmax) / 2d, 0),
                        (double)Math.Round((gmin + gmax) / 2d, 2),
                        (double)f,
                        balance,
                        itemRevers,
                        coefficientFirst,
                        coefficientSecond
                    );

                    setups.Add(setup);


                }
            }
            else
            {
                var fileSetup = new FileInfo(throwPath);
                var streamreader = new StreamReader(fileSetup.FullName);
                var setupOld = streamreader.ReadLine().Split('\t');

                Invoke((MethodInvoker)delegate ()
                {
                    for (var i = 0; i < textBoxs.Count; i++)
                        if (i != 26 && i != 27)
                        {

                            textBoxs[i].Text = setupOld[i].ToString();
                        }

                    for (var i = 0; i < checkBoxs.Count; i++)
                        checkBoxs[i].Checked = bool.Parse(setupOld[i + textBoxs.Count]);

                    VriantsAll_CB.Checked = true;
                    LevelFilter_CB.Checked = false;
                    CoefficientFilter_CB.Checked = false;
                    ClearProfitFilter_CB.Checked = false;
                });

                streamreader.ReadLine();
                streamreader.ReadLine();

                while (!streamreader.EndOfStream)
                {
                    var input = streamreader.ReadLine().Split('\t');

                    var result = new ResultClass
                    {
                        marginMax = input[0].ToDouble(),
                        massMin = input[1].ToDouble(),
                        mass = input[2].ToDouble(),
                        margin = 0,
                        profit = input[3].ToDouble(),
                        levelMin = input[5].ToDouble(),
                        coefficient = input[6].ToDouble(),
                        ordersOpenedCount1 = input[7].ToInt(),
                        ordersClosedCount1 = input[8].ToInt(),
                        ordersOpenedCount2 = input[9].ToInt(),
                        ordersClosedCount2 = input[10].ToInt()
                    };

                    var setup = new Setup
                    (
                        input[11].ToDouble(),
                        input[12].ToDouble(),
                        input[13].ToDouble(),
                        input[16].ToInt(),
                        input[20].ToInt(),
                        input[21].ToInt(),
                        input[17].ToInt(),
                        input[14].ToDouble(),
                        input[15].ToDouble(),
                        input[22].ToDouble(),
                        input[23].ToInt(),
                        input[18].ToDouble(),
                        input[19].ToDouble()
                    );

                    setups.Add(setup);
                    resultsOld.Add(result, setup);
                }
            }

            setups.Distinct();

            List<Tick> tickList;

            if (tickListMain == null)
            {
                var inputList = (List<string>)LoadRealGraphic(file);
                tickList = ConvertToTickList(inputList, itemRevers);
                tickListMain = tickList;
                inputList.Clear();
            }
            else
                tickList = tickListMain;

            progress = 0;
            var startResult = new Result[setups.Count];
            var tickArrays = tickList.ToArray();
            var setupsArray = setups.ToArray();

            var maxArray = new double[] { amax, bmax, cmax, dmax, gmax, fmax };
            var minArray = new double[] { amin, bmin, cmin, dmin, gmin, fmin };
            var stepArray = new double[] { astep, bstep, cstep, dstep, gstep, fstep };
            var errorCount = 0;
            progressLimit = setupsArray.Length;

            var treadCount = new ParallelOptions();
            var resultDic = new Dictionary<ResultClass, Setup>();
            var locker = new Object();

            stream = AgregateStart(file.Name.Split('.')[0]);
            resultPairDic = new Dictionary<ResultClass, Setup>();

            Parallel.For(0, setupsArray.Length, new ParallelOptions() { MaxDegreeOfParallelism = 24 }, i =>
            {
            //    for (int i = 0; i < setupsArray.Length; i++)
            //{
                try
                {
                    var resultTemporary = new ResultClass();
                    var setupNew = setupsArray[i].Copy();

                    var resultPair = new Result[2];

                    while (threadSleeper)
                        Thread.Sleep(5000);

                    if (AlgorithmType_CB.Checked)
                    {
                        var iterationNew = new IterationNew(setupsArray[i].Copy());
                        resultPair = iterationNew.SimulateMathod(setupsArray[i].Copy(), tickArrays);
                    }
                    else
                    {
                        var iterationNew = new IterationOld(setupsArray[i].Copy());
                        resultPair = iterationNew.SimulateMathod(setupsArray[i].Copy(), tickArrays);
                    }

                    var startResult1 = resultPair[0];
                    var startResult2 = resultPair[1];

                    var clearProfit1 = startResult1.profit + startResult1.mass;
                    var clearProfit2 = startResult2.profit + startResult2.mass;

                    double coefficient;
                    if (clearProfit1 == 0 || clearProfit2 == 0)
                        coefficient = 200000d;
                    else if (clearProfit1 < 0 && clearProfit2 < 0)
                        coefficient = 200000d;
                    else
                        coefficient = Math.Min(Math.Abs(clearProfit1), Math.Abs(clearProfit2)) / Math.Max(Math.Abs(clearProfit1), Math.Abs(clearProfit2));

                    Dictionary<ResultClass, Setup> resultNew;
                    if (VriantsAll_CB.Checked)
                    {
                        resultNew = new Dictionary<ResultClass, Setup>
                        {
                            {
                                new ResultClass()
                                {
                                    levelMin = Math.Min(startResult1.levelMin, startResult2.levelMin),
                                    margin = startResult2.margin,
                                    marginMax = Math.Max(startResult1.marginMax, startResult2.marginMax),
                                    mass = startResult1.mass + startResult2.mass,
                                    massMin = Math.Min(startResult1.massMin, startResult2.massMin),
                                    profit = startResult1.profit + startResult2.profit,
                                    coefficient = coefficient,
                                    ordersOpenedCount1 = startResult1.ordersOpenedCount1,
                                    ordersClosedCount1 = startResult1.ordersClosedCount1,
                                    ordersOpenedCount2 = startResult2.ordersOpenedCount2,
                                    ordersClosedCount2 = startResult2.ordersClosedCount2
                                },

                                setupNew.Copy()
                            }
                        };

                        if (LevelFilter_CB.Checked)
                        {
                            if (LevelType_RB.Checked)
                                resultNew = ReliseLevelOld(resultNew, tickListMain);
                            else
                                resultNew = ReliseLevelNew(resultNew, tickListMain);
                        }
                    }
                    else
                        resultNew = SearchOptimum(coefficient, setupNew, resultTemporary, minArray, maxArray, stepArray);

                    lock (locker)
                    {
                        for (var r = 0; r < resultNew.Count; r++)
                        {
                            var result1 = resultNew.ElementAt(r);
                            var switcher = true;
                            for (var s = 0; s < resultPairDic.Count; s++)
                            {
                                var result2 = resultPairDic.ElementAt(s);
                                if (result1.Key.Equals(result1.Key, result2.Key) && result1.Value.Equals(result1.Value, result2.Value))
                                {
                                    resultNew.Remove(resultNew.ElementAt(r).Key);
                                    if (r >= resultNew.Count)
                                        r--;
                                    switcher = false;
                                    break;
                                }
                            }

                            if (switcher)
                                resultPairDic.Add(resultNew.ElementAt(r).Key, resultNew.ElementAt(r).Value);
                        }
                        AgregateRecord(resultNew, stream);

                        progress++;
                    }

                }
                catch
                {
                    lock (locker)
                    {
                        progress++;
                        errorCount++;
                    }
                }
            });
            //}

            var timeSpain = stopwatch.Elapsed;
            var timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpain.Hours, timeSpain.Minutes, timeSpain.Seconds);
            var top = resultPairDic.OrderBy(x => x.Value.HashCode).ToDictionary(x => x.Key, x => x.Value);
            
            resultsOld.OrderBy(x => x.Value.HashCode).ToList();

            stream.Close();
            stream = new StreamWriter(pathToFile);
            stream.BaseStream.Position = 0;

            var id = "";
            foreach (var tb in textBoxs)
                id += tb.Text + "\t";

            foreach (var cb in checkBoxs)
                id += cb.Checked + "\t";
            stream.WriteLine(id);

            if (throwPath == null)
            {
                stream.WriteLine(String.Format("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}",
                    "MM, c",
                    "mm, c",
                    "Mass, c",
                    "Profit, c",
                    "Clear Profit, c",
                    "Level, %",
                    "Coefficient",
                    "Op. Ord. 1",
                    "Cl. Ord. 1",
                    "Op. Ord. 2",
                    "Cl. Ord. 2",
                    "Lot",
                    "LOC",
                    "LSC",
                    "POC",
                    "PSC",
                    "TP",
                    "Start Pipstep",
                    "First Coefficient",
                    "Second Coefficient",
                    "Strat. Count",
                    "Orders Count",
                    "Balance",
                    "Accurasity"
                    ));
            }
            else
            {
                stream.WriteLine(String.Format("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\t{18}\t{19}\t{20}\t{21}\t{22}\t{23}\t{24}\t{25}\t{26}",
                    "MM, c",
                    "mm, c",
                    "Mass, c",
                    "Profit, c",
                    "Clear Profit, c",
                    "Level, %",
                    "Coefficient",
                    "Op. Ord. 1",
                    "Cl. Ord. 1",
                    "Op. Ord. 2",
                    "Cl. Ord. 2",
                    "Lot",
                    "LOC",
                    "LSC",
                    "POC",
                    "PSC",
                    "TP",
                    "Start Pipstep",
                    "First Coefficient",
                    "Second Coefficient",
                    "Strat. Count",
                    "Orders Count",
                    "Balance",
                    "Accurasity",
                    "Clear Profit Deltha",
                    "Coefficient Deltha",
                    "Level Deltha"
                    ));
            }

            AgregateRecord(top, stream);

            stopwatch.Stop();
            stream.Close();

            try { progressThread.Abort(); } catch { }

            InterfaceOn();

            if (errorCount > 0)
                MessageBox.Show("Итераций пропущено: " + errorCount.ToString());

            MessageBox.Show(timeString);

            try { mainThread.Abort(); } catch { }
            InterfaceOn();
        }

        private void InterfaceOn()
        {
            Invoke((MethodInvoker)delegate ()
            {
                foreach (var tb in textBoxs)
                    tb.Enabled = true;

                foreach (var cb in checkBoxs)
                    cb.Enabled = true;

                foreach (var b in buttons)
                    b.Enabled = true;

                Stop_B.Enabled = false;
                Pause_B.Enabled = false;
                Continue_B.Enabled = false;
                Calculate_B.Text = "Начать расчёт";
                Progress_PB.Value = 0;

                if (!ClearProfitFilter_CB.Checked)
                    MarjenLimit_TB.Enabled = false;

                if (!LevelFilter_CB.Checked)
                {
                    LevelLimit_TB.Enabled = false;
                    TestCount_TB.Enabled = false;
                    TestDroprate_TB.Enabled = false;
                }

                if (!CoefficientFilter_CB.Checked)
                    CoefficientLimit_TB.Enabled = false;
            });
        }

        private void InterfaceOff()
        {
            progress = 0;
            progressLimit = 0;
            progressLast = 0;
            timeString = "--:--:--";
            speedList = null;
            timeString = "";
            stopwatch = null;
            stream = null;

            Invoke((MethodInvoker)delegate ()
            {
                foreach (var tb in textBoxs)
                    tb.Enabled = false;

                foreach (var cb in checkBoxs)
                    cb.Enabled = false;

                foreach (var b in buttons)
                    b.Enabled = false;

                Stop_B.Enabled = true;
                Pause_B.Enabled = true;

            });
        }

        private void Open_B_Click(object sender, EventArgs e)
        {
            tickListMain = null;

            using (var ofd1 = new OpenFileDialog())
            {
                ofd1.Filter = "txt files (*.txt)|*.txt";
                if (ofd1.ShowDialog() == DialogResult.OK)
                    GraphicPath_TB.Text = ofd1.FileName;
            }
        }

        private void Stop_B_Click(object sender, EventArgs e)
        {
            if (mainThread != null)
            try { mainThread.Abort(); } catch { }

            if (progressThread != null)
            try { progressThread.Abort(); } catch { }

            if (testThread != null)
                try { testThread.Abort(); } catch { }

            stopwatch.Stop();
            stream.Close();

            InterfaceOn();

            Progress_PB.Value = 0;
            Calculate_B.Text = "Начать расчёт";
        }

        private void SetupSave_B_Click(object sender, EventArgs e)
        {
            var path = "";

            using (var ofd1 = new SaveFileDialog())
            {
                ofd1.Filter = "setup files (*.setup)|*.setup";
                if (ofd1.ShowDialog() == DialogResult.OK)
                    path = ofd1.FileName;
            }

            try
            {
                var file = new StreamWriter(path);

                foreach (var tb in textBoxs)
                    file.WriteLine(tb.Text);

                foreach (var cb in checkBoxs)
                    file.WriteLine(cb.Checked);

                file.Close();
            }
            catch { }
        }

        private void SetupLoad_B_Click(object sender, EventArgs e)
        {
            var path = "";

            using (var ofd1 = new OpenFileDialog())
            {
                ofd1.Filter = "setup files (*.setup)|*.setup";
                if (ofd1.ShowDialog() == DialogResult.OK)
                    path = ofd1.FileName;
            }

            try
            {
                var file = new StreamReader(path);

                foreach (var tb in textBoxs)
                    tb.Text = file.ReadLine();

                foreach (var cb in checkBoxs)
                    cb.Checked = bool.Parse(file.ReadLine());

                file.Close();
            }
            catch { }
        }

        private void Filter_CB_CheckedChanged(object sender, EventArgs e)
        {
            MarjenLimit_TB.Enabled = ClearProfitFilter_CB.Checked;
        }

        private void ResultOpen_B_Click(object sender, EventArgs e)
        {
            using (var ofd1 = new FolderBrowserDialog())
            {
                if (ofd1.ShowDialog() == DialogResult.OK)
                    ResultPath_TB.Text = ofd1.SelectedPath;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            IterationCount_L.Text = GetIterationCount().ToString();
        }

        private void Pause_B_Click(object sender, EventArgs e)
        {
            threadSleeper = true;
            Pause_B.Enabled = false;
            Continue_B.Enabled = true;
        }

        private void Continue_B_Click(object sender, EventArgs e)
        {
            threadSleeper = false;
            Continue_B.Enabled = false;
            Pause_B.Enabled = true;
        }

        private void CoefficientFilter_CB_CheckedChanged(object sender, EventArgs e)
        {
            CoefficientLimit_TB.Enabled = CoefficientFilter_CB.Checked;
        }

        private void LevelFilter_CB_CheckedChanged(object sender, EventArgs e)
        {
            LevelLimit_TB.Enabled = LevelFilter_CB.Checked;
            TestCount_TB.Enabled = LevelFilter_CB.Checked;
            TestDroprate_TB.Enabled = LevelFilter_CB.Checked;
        }

        private void Test_B_Click(object sender, EventArgs e)
        {
            testThread = new Thread(new ThreadStart(Test)) { Priority = ThreadPriority.Lowest };
            testThread.Start();
        }

        public void Test()
        {
            InterfaceOff();

            var amin = LOCMin_TB.Text.ToDouble(); var amax = LOCMax_TB.Text.ToDouble(); var astep = LOCStep_TB.Text.ToDouble(); var acount = (amax - amin) / astep + 1;
            var bmin = LSCMin_TB.Text.ToDouble(); var bmax = LSCMax_TB.Text.ToDouble(); var bstep = LSCStep_TB.Text.ToDouble(); var bcount = (bmax - bmin) / bstep + 1;
            var cmin = TakeprofitMin_TB.Text.ToInt(); var cmax = TakeprofitMax_TB.Text.ToInt(); var cstep = TakeprofitStep_TB.Text.ToInt(); var ccount = (cmax - cmin) / cstep + 1;
            var dmin = StartPipstepMin_TB.Text.ToInt(); var dmax = StartPipstepMax_TB.Text.ToInt(); var dstep = StartPipstepStep_TB.Text.ToInt(); var dcount = (dmax - dmin) / dstep + 1;
            var gmin = POCMin_TB.Text.ToDouble(); var gmax = POCMax_TB.Text.ToDouble(); var gstep = POCStep_TB.Text.ToDouble(); var gcount = (gmax - gmin) / gstep + 1;
            var fmin = PSCMin_TB.Text.ToDouble(); var fmax = PSCMax_TB.Text.ToDouble(); var fstep = PSCStep_TB.Text.ToDouble(); var fcount = (fmax - fmin) / fstep + 1;

            progressLimit = TestCount_TB.Text.ToInt();
            progress = 0;
            progressThread = new Thread(new ThreadStart(Progress))
            {
                Priority = ThreadPriority.Highest
            };
            progressThread.Start();

            var step = Lot_TB.Text.ToDouble();
            var coefficientDroprate = TestDroprate_TB.Text.ToDouble();
            var count = TestCount_TB.Text.ToInt();

            var iterationMax = Math.Round(acount * bcount * ccount * dcount * gcount * fcount);
            var numberStep = iterationMax / count;

            int ordersCount = OrdersCount_TB.Text.ToInt();
            int strategyCount = StrategyCount_TB.Text.ToInt();
            int itemRevers = MathPow(10, Accurasity_TB.Text.ToInt());

            double lotSize = Lot_TB.Text.ToDouble();
            double balance = Balance_TB.Text.ToDouble();
            double coefficientFirst = CoefficientFirst_TB.Text.ToDouble();
            double coefficientSecond = CoefficientSecond_TB.Text.ToDouble();

            Tick[] tickArrays;
            if (tickListMain == null)
            {
                var inputList = (List<string>)LoadRealGraphic(new FileInfo(GraphicPath_TB.Text));
                tickArrays = ConvertToTickList(inputList, itemRevers).ToArray();
                inputList = null;
                tickListMain = tickArrays.ToList();
            }
            else
                tickArrays = tickListMain.ToArray();

            var speedList = new List<double>();
            var stopwatch = Stopwatch.StartNew();
            var locker = new object();
            var maxArray = new double[] { amax, bmax, cmax, dmax, gmax, fmax };
            var minArray = new double[] { amin, bmin, cmin, dmin, gmin, fmin };
            var stepArray = new double[] { astep, bstep, cstep, dstep, gstep, fstep };
            stopwatch.Start();

            Parallel.For(0, count, i =>
            {
            //    for (var i = 0; i < count; i++)
            //{
                var number = (int)Math.Ceiling(numberStep * i);

                var finteger = (int)(number / fcount);
                var fvalue = fmin + (number / fcount - finteger) * fcount * fstep;

                var ginteger = (int)(finteger / gcount);
                var gvalue = gmin + (finteger / gcount - ginteger) * gcount * gstep;

                var dinteger = (int)(ginteger / dcount);
                var dvalue = dmin + (ginteger / dcount - dinteger) * dcount * dstep;

                var cinteger = (int)(dinteger / ccount);
                var cvalue = cmin + (dinteger / ccount - cinteger) * ccount * cstep;

                var binteger = (int)(cinteger / bcount);
                var bvalue = bmin + (cinteger / bcount - binteger) * bcount * bstep;

                var ainteger = (int)(binteger / acount);
                var avalue = amin + (binteger / acount - ainteger) * acount * astep;

                var setup = new Setup
                (
                    lotSize,
                    avalue,
                    bvalue,
                    cvalue,
                    strategyCount,
                    ordersCount,
                    dvalue,
                    gvalue,
                    fvalue,
                    balance,
                    itemRevers,
                    coefficientFirst,
                    coefficientSecond
                );

                var resultTemporary = new ResultClass();
                var setupNew = setup.Copy();

                while (threadSleeper)
                    Thread.Sleep(5000);

                Result[] resultPair;
                if (AlgorithmType_CB.Checked)
                {
                    var iterationNew = new IterationNew(setup.Copy());
                    resultPair = iterationNew.SimulateMathod(setup.Copy(), tickArrays.ToArray());
                }
                else
                {
                    var iterationNew = new IterationOld(setup.Copy());
                    resultPair = iterationNew.SimulateMathod(setup.Copy(), tickArrays.ToArray());
                }

                var startResult1 = resultPair[0];
                var startResult2 = resultPair[1];

                var clearProfit1 = startResult1.profit + startResult1.mass;
                var clearProfit2 = startResult2.profit + startResult2.mass;

                double coefficient;
                if (clearProfit1 == 0 || clearProfit2 == 0)
                    coefficient = 200000d;
                else if (clearProfit1 < 0 && clearProfit2 < 0)
                    coefficient = 200000d;
                else
                    coefficient = Math.Min(Math.Abs(clearProfit1), Math.Abs(clearProfit2)) / Math.Max(Math.Abs(clearProfit1), Math.Abs(clearProfit2));

                Dictionary<ResultClass, Setup> resultNew;
                if (VriantsAll_CB.Checked)
                {
                    resultNew = new Dictionary<ResultClass, Setup>
                    {
                        {
                            new ResultClass()
                            {
                                levelMin = Math.Min(startResult1.levelMin, startResult2.levelMin),
                                margin = startResult2.margin,
                                marginMax = Math.Max(startResult1.marginMax, startResult2.marginMax),
                                mass = startResult1.mass + startResult2.mass,
                                massMin = Math.Min(startResult1.massMin, startResult2.massMin),
                                profit = startResult1.profit + startResult2.profit,
                                coefficient = coefficient,
                                ordersOpenedCount1 = startResult1.ordersOpenedCount1,
                                ordersClosedCount1 = startResult1.ordersClosedCount1,
                                ordersOpenedCount2 = startResult2.ordersOpenedCount2,
                                ordersClosedCount2 = startResult2.ordersClosedCount2
                            },
                            setup.Copy()
                        }
                    };

                    if (LevelFilter_CB.Checked)
                    {
                        if (LevelType_RB.Checked)
                            resultNew = ReliseLevelOld(resultNew, tickListMain);
                        else
                            resultNew = ReliseLevelNew(resultNew, tickListMain);
                    }
                }
                else
                    resultNew = SearchOptimum(coefficient, setup, resultTemporary, minArray, maxArray, stepArray);

                lock (locker)
                {
                    var ts = stopwatch.ElapsedMilliseconds;
                    speedList.Add(ts);
                    progress++;
                }
            });
        //}

            stopwatch.Stop();

            var time = speedList.Average();
            GC.Collect();

            try { progressThread.Abort(); } catch { }
            InterfaceOn();

            MessageBox.Show("Avarage time = " + Math.Round(time).ToString() + ", ms");
        }

        private void GraphicPath_TB_TextChanged(object sender, EventArgs e)
        {
            tickListMain = null;
        }
    }
}

namespace Iteration
{
    public struct IterationOld
    {
        public int ItemRevers;
        public double itemReversForGetProfit;

        public ArrayList<int> items;
        public ArrayList<bool> buyTypes;
        public ArrayList<bool> timers;
        public ArrayList<bool> strategySequence;
        public ArrayListList<int> orders;

        public ArrayListC<Order> openedOrdersList;
        public ArrayListC<Order> closedOrdersList;
        public Dictionary<int, Order> openedOrdersDic;
        public Dictionary<int, Order> closedOrdersDic;
        public Dictionary<int, double[]> levelDictionary;
        public Order selectedOrder;
        public Setup setup;

        public double profitGlobal;
        public double massGlobal;
        public double marginGlobal;
        public double marginMaxGlobal;
        public double massMinGlobal;
        public double levelMinGlobal;

        public int time;

        public IterationOld(Setup options)
        {
            ItemRevers = options.accurasity;
            setup = options;
            items = options.items;
            buyTypes = options.buyTypes;
            timers = options.timers;
            strategySequence = options.strategySequence;
            orders = options.orders;
            openedOrdersList = options.openedOrders;
            closedOrdersList = options.closedOrders;
            openedOrdersDic = new Dictionary<int, Order>();
            closedOrdersDic = new Dictionary<int,Order>();
            levelDictionary = new Dictionary<int, double[]>();
            selectedOrder = new Order();
            time = 0;
            itemReversForGetProfit = 100000 / ItemRevers * options.coefficientSecond;

            profitGlobal = 0;
            massGlobal = 0;
            marginGlobal = 0;
            marginMaxGlobal = double.MinValue;
            massMinGlobal = double.MaxValue;
            levelMinGlobal = double.MaxValue;

            for (var i = 0; i < options.strategyCount * 2; i++)
            {
                levelDictionary.Add(i, new double[]
                {
                    Math.Round(Math.Pow(setup.PSC, i / 2), 2),
                    Math.Round(Math.Pow(setup.LSC, i / 2), 2)
                });
            }
        }

        private int OrderSend(OrderType type, double lot, int takeprofitPrice, Setup setup)
        {
            int openPrise;
            if (type == OrderType.Buy)
                openPrise = setup.marketAsk;
            else
                openPrise = setup.marketBid;

            Order order = new Order
            {
                ticket = setup.ticket + 1,
                type = type,
                lot = Math.Round(lot, 2),
                openPrice = openPrise,
                openTime = time,
                taikeprofitPrice = takeprofitPrice
            };

            order.lastPrise = order.openPrice;
            order.closeTime = int.MinValue;
            openedOrdersList.Add(order);
            openedOrdersDic.Add(order.ticket, order);

            return order.ticket;
        }

        private bool OrderModify(int ticket, int newTakeprofit)
        {
            Order order;
            for (var i = 0; i < openedOrdersList.count[0]; i++)
                if (ticket == openedOrdersList.array[i].ticket)
                {
                    order = openedOrdersList.array[i];
                    order.taikeprofitPrice = newTakeprofit;
                    openedOrdersList.array[i] = order;
                    openedOrdersDic[ticket] = order;
                    return true;
                }

            return false;
        }

        private int OnInit(Setup setup)
        {
            for (int i = 0; i < setup.strategyCount; i++)
            {
                timers.Add(false);

                if (i % 2 != 0)
                {
                    buyTypes.Add(true);
                    items.Add(2147483647);
                }
                else
                {
                    buyTypes.Add(false);
                    items.Add(0);
                }
            }

            strategySequence.Add(false);
            strategySequence.Add(false);
            strategySequence.Add(false);

            return MainMethod(setup);
        }

        private int OnTick(Setup setup)
        {
            time++;

            return MainMethod(setup);
        }

        private int MainMethod(Setup setup)
        {
            int ticket = -1;

            for (short i = 0; i < setup.strategyCount; i++)
            {
                if (ticket != -1)
                    setup.ticket = ticket;

                OrdersCheckClear(i);

                bool openMainFirst;
                //Гарантированное открытие первого ордера первой стратегии и второй стратегии
                if (orders.count[0] <= 0)
                {
                    openMainFirst = true;
                    strategySequence.array[i] = true;
                }
                else
                    openMainFirst = false;



                //Разрешение на открытие первого ордера последующих стратегий
                if (i > 0)
                {
                    if (orders.count[i - 1] >= 2)
                        openMainFirst = true;
                    else
                        openMainFirst = false;
                }

                var preArray = levelDictionary[i];
                double pipstepCoefficient = preArray[0];
                double lotCoefficient = preArray[1];

                strategySequence.array[i] = true;

                //Открытие первого ордера
                if (openMainFirst)
                    if (orders.count[i] <= 0)
                        if (buyTypes.array[i])
                            ticket = OrderOpenUp(i, pipstepCoefficient, lotCoefficient, setup);
                        else
                            ticket = OrderOpenDown(i, pipstepCoefficient, lotCoefficient, setup);

                //Открытие последующих ордеров
                if (orders.count[i] > 0)
                    if (orders.count[i] < setup.ordersCount)
                    {
                        int ask = setup.marketAsk;
                        int bid = setup.marketBid;

                        if (buyTypes.array[i])
                        {
                            if (ask <= items.array[i])
                                ticket = OrderOpenUp(i, pipstepCoefficient, lotCoefficient, setup);
                        }
                        else if (!buyTypes.array[i])
                            if (bid >= items.array[i])
                                ticket = OrderOpenDown(i, pipstepCoefficient, lotCoefficient, setup);
                    }
            }

            return ticket; //Только для C#
        }

        private bool SelectOrder(int index, bool positionType, bool historyType)
        {
            Dictionary<int, Order> dictionary;
            if (historyType)
                dictionary = closedOrdersDic;
            else
                dictionary = openedOrdersDic;

            if (positionType)
            {
                selectedOrder = dictionary.ElementAt(index).Value;
                return true;
            }
            else if (dictionary.ContainsKey(index))
            {
                selectedOrder = dictionary[index];
                return true;
            }
            else
                return false;
        }

        private void OrdersCheckClear(short index)
        {
            for (int i = 0; i < orders.count[index]; i++)
            {
                if (!SelectOrder(orders.array[index, i], false, false))
                {
                    orders.RemoveAt(index, i);
                    if (buyTypes.array[i])
                        items.array[index] = int.MaxValue;
                    else
                        items.array[index] = int.MinValue;
                    i--;
                }
            }
        }

        private int OrderOpenUp(short index, double pipstepCoefficient, double lotCoefficient, Setup setup)
        {
            double lot = Math.Round(setup.lot * lotCoefficient * Math.Pow(setup.LOC, orders.count[index]), 2);

            if (Math.Round(lot, 2) < 0.01)
                lot = 0.01;

            var ask = setup.marketAsk;
            var orderID = OrderSend(OrderType.Buy, lot, ask + setup.TP, setup);

            orders.Add(index, orderID); 

            items.array[index] = (int)(ask - setup.startPipstep * Math.Pow(setup.POC, orders.count[index] - 1) * pipstepCoefficient);
            if (items.array[index] == ask)
                items.array[index] -= 1;

            OrdersModify(index);

            return orderID;
        }

        private int OrderOpenDown(short index, double pipstepCoefficient, double lotCoefficient, Setup setup)
        {
            double lot = Math.Round(setup.lot * lotCoefficient * Math.Pow(setup.LOC, orders.count[index]), 2);

            if (Math.Round(lot, 2) < 0.01)
                lot = 0.01;

            var bid = setup.marketBid;
            var orderID = OrderSend(OrderType.Sell, lot, bid - setup.TP, setup);

            orders.Add(index, orderID);

            items.array[index] = (int)(bid + setup.startPipstep * Math.Pow(setup.POC, orders.count[index] - 1) * pipstepCoefficient);
            if (items.array[index] == bid)
                items.array[index] += 1;

            OrdersModify(index);

            return orderID;
        }

        private void OrdersModify(int index)
        {
            if (orders.count[index] <= 0) return;

            double numerator = 0;
            double denumerator = 0;

            for (int i = 0; i < orders.count[index]; i++)
            {
                double lot = 0;
                double taikprofit = 0;

                if (SelectOrder(orders.array[index, i], false, false))
                {
                    lot = selectedOrder.lot;
                    taikprofit = selectedOrder.taikeprofitPrice;

                    //if ((lot - 0.00000001) > 0)
                    //    if ((taikprofit - 0.00000001) > 0)
                    //        break;
                }

                if (Math.Round(lot, 2)<0.0)
                    return;

                numerator += taikprofit * lot;
                denumerator += lot;
            }

            int newTaikprofit = (int)Math.Round(numerator / denumerator, 0);

            

            for (int i = 0; i < orders.count[index]; i++)
            {
                if (!SelectOrder(orders.array[index, i], false, false))
                {
                    if (!SelectOrder(selectedOrder.ticket, false, true))
                        OrderModify(selectedOrder.ticket, newTaikprofit);
                }
                else
                    OrderModify(selectedOrder.ticket, newTaikprofit);
            }
        }

        public Result[] SimulateMathod(Setup setup, Tick[] tickList)
        {
            double profit = 0;
            double mass = 0;
            double margin = 0;
            double marginMax = double.MinValue;
            double massMin = double.MaxValue;
            double levelMin = double.MaxValue;
            int openedCount = 0;
            int closedCount = 0;
            Order order;
            Result result1 = new Result();
            Result result2;
            double opt = 100 * setup.coefficientFirst;
            setup.marketBid = tickList[time].buy;
            setup.marketAsk = tickList[time].sell;

            var newTicket = OnInit(setup);

            if (newTicket != -1)
                setup.ticket = newTicket;

            while (time < tickList.Length)
            {
                double marginTemporary = 0;
                double massTemporary = 0;

                setup.marketBid = tickList[time].buy;
                setup.marketAsk = tickList[time].sell;

                for (var i = 0; i < openedOrdersList.count[0]; i++)
                {
                    order = openedOrdersList.array[i];

                    if (order.type == OrderType.Buy)
                    {
                        marginTemporary += order.lot * opt;

                        var profitBuy = order.GetProfitBuy(setup.marketBid, itemReversForGetProfit);
                        massTemporary += profitBuy;

                        if (order.taikeprofitPrice <= setup.marketBid)
                        {
                            order.CloseOrder(setup.marketBid, openedOrdersList, closedOrdersList, openedOrdersDic, closedOrdersDic, this.time);
                            i--;
                            profit += profitBuy;
                        }
                    }
                    else
                    {
                        marginTemporary -= order.lot * opt;

                        var profitSell = order.GetProfitSell(setup.marketAsk, itemReversForGetProfit);
                        massTemporary += profitSell;

                        if (order.taikeprofitPrice >= setup.marketAsk)
                        {
                            order.CloseOrder(setup.marketAsk, openedOrdersList, closedOrdersList, openedOrdersDic, closedOrdersDic, this.time);
                            i--;
                            profit += profitSell;
                        }
                    }
                }

                double marginAbs = Math.Abs(marginTemporary);

                if (marginMax < marginAbs)
                    marginMax = marginAbs;

                if (massMin > massTemporary)
                    massMin = massTemporary;

                if (marginAbs <= 0)
                    marginAbs = 0.1;

                var levelNew = (setup.balance + profit + massTemporary) / marginAbs;
                if (massTemporary != 0.0)
                    if (levelMin > levelNew)
                        levelMin = levelNew;

                if (time == tickList.Length / 2)
                {
                    foreach (var o in openedOrdersList.array)
                    {
                        if (o.type == OrderType.Buy)
                        {
                            margin += o.lot * opt;
                            mass += o.GetProfitBuy(setup.marketBid, itemReversForGetProfit);
                        }
                        else
                        {
                            margin -= o.lot * opt;
                            mass += o.GetProfitSell(setup.marketAsk, itemReversForGetProfit);
                        }

                        openedCount++;
                    }

                    if (margin <= 0)
                        margin = 0.1;

                    foreach (var o in closedOrdersList.array)
                        closedCount++;


                    result1 = new Result()
                    {
                        profit = profit,
                        mass = mass,
                        margin = margin,
                        marginMax = marginMax,
                        massMin = massMin,
                        levelMin = Math.Round(levelMin, 2) * 100,
                        ordersOpenedCount1 = openedCount,
                        ordersClosedCount1 = closedCount
                    };

                    mass = 0;
                    margin = 0;
                    openedCount = 0;
                    closedCount = 0;
                }

                newTicket = OnTick(setup);

                if (newTicket != -1)
                    setup.ticket = newTicket;
            }

            foreach (var o in openedOrdersList.array)
            {
                if (o.type == OrderType.Buy)
                {
                    margin += o.lot * opt;
                    mass += o.GetProfitBuy(setup.marketBid, itemReversForGetProfit);
                }
                else
                {
                    margin -= o.lot * opt;
                    mass += o.GetProfitSell(setup.marketAsk, itemReversForGetProfit);
                }

                openedCount++;
            }

            if (margin <= 0)
                margin = 0.1;

            foreach (var o in closedOrdersList.array)
                closedCount++;

            result2 = new Result()
            {
                profit = profit - result1.profit,
                mass = mass - result1.mass,
                margin = margin,
                marginMax = marginMax,
                massMin = massMin,
                levelMin = Math.Round(levelMin, 2) * 100,
                ordersOpenedCount2 = openedCount,
                ordersClosedCount2 = closedCount - result1.ordersClosedCount1
            };

            return new Result[] { result1, result2 };
        }
    }

    public struct IterationNew
    {
        public int ItemRevers;
        public double itemReversForGetProfit;

        public ArrayList<int> items;
        public ArrayList<bool> buyTypes;
        public ArrayList<bool> timers;
        public ArrayList<bool> strategySequence;
        public ArrayListList<int> orders;

        public ArrayListC<Order> openedOrdersList;
        public ArrayListC<Order> closedOrdersList;
        public Dictionary<int, Order> openedOrdersDic;
        public Dictionary<int, Order> closedOrdersDic;
        public Dictionary<int, double[]> levelDictionary;
        public Order selectedOrder;
        public Setup setup;

        public double profitGlobal;
        public double massGlobal;
        public double marginGlobal;
        public double marginMaxGlobal;
        public double massMinGlobal;
        public double levelMinGlobal;

        public int time;

        public IterationNew(Setup options)
        {
            ItemRevers = options.accurasity;
            setup = options;
            items = options.items;
            buyTypes = options.buyTypes;
            timers = options.timers;
            strategySequence = options.strategySequence;
            orders = options.orders;
            openedOrdersList = options.openedOrders;
            closedOrdersList = options.closedOrders;
            openedOrdersDic = new Dictionary<int, Order>();
            closedOrdersDic = new Dictionary<int, Order>();
            levelDictionary = new Dictionary<int, double[]>();
            selectedOrder = new Order();
            time = 0;
            itemReversForGetProfit = 100000 / ItemRevers * options.coefficientSecond;

            profitGlobal = 0;
            massGlobal = 0;
            marginGlobal = 0;
            marginMaxGlobal = double.MinValue;
            massMinGlobal = double.MaxValue;
            levelMinGlobal = double.MaxValue;

            for (var i = 0; i < options.strategyCount * 2; i++)
            {
                levelDictionary.Add(i, new double[]
                {
                    Math.Round(Math.Pow(setup.PSC, i / 2), 2),
                    Math.Round(Math.Pow(setup.LSC, i / 2), 2)
                });
            }
        }

        private int OrderSend(OrderType type, double lot, int takeprofitPrice, Setup setup)
        {
            int openPrise;
            if (type == OrderType.Buy)
                openPrise = setup.marketAsk;
            else
                openPrise = setup.marketBid;

            Order order = new Order
            {
                ticket = setup.ticket + 1,
                type = type,
                lot = Math.Round(lot, 2),
                openPrice = openPrise,
                openTime = time,
                taikeprofitPrice = takeprofitPrice
            };

            order.lastPrise = order.openPrice;
            order.closeTime = int.MinValue;
            openedOrdersList.Add(order);
            openedOrdersDic.Add(order.ticket, order);

            return order.ticket;
        }

        private bool OrderModify(int ticket, int newTakeprofit)
        {
            Order order;
            for (var i = 0; i < openedOrdersList.count[0]; i++)
                if (ticket == openedOrdersList.array[i].ticket)
                {
                    order = openedOrdersList.array[i];
                    order.taikeprofitPrice = newTakeprofit;
                    openedOrdersList.array[i] = order;
                    openedOrdersDic[ticket] = order;
                    return true;
                }

            return false;
        }

        private int OnInit(Setup setup)
        {
            for (int i = 0; i < setup.strategyCount; i++)
            {
                timers.Add(true);
                if (i % 2 == 0)
                {
                    buyTypes.Add(true);
                    items.Add(int.MaxValue);
                }
                else
                {
                    buyTypes.Add(false);
                    items.Add(int.MinValue);
                }

                strategySequence.Add(false);
            }

            strategySequence.Add(false);
            strategySequence.Add(false);
            strategySequence.Add(false);

            return MainMethod(setup);
        }

        private int OnTick(Setup setup)
        {
            time++;

            return MainMethod(setup);
        }

        private int MainMethod(Setup setup)
        {
            int ticket = -1;

            strategySequence.array[0] = true;
            strategySequence.array[1] = true;

            for (short i = 0; i < setup.strategyCount; i++)
            {
                if (ticket != -1)
                    setup.ticket = ticket;

                OrdersCheckClear(i);

                bool openMainFirst;
                //Гарантированное открытие первого ордера первой стратегии и второй стратегии
                if (orders.count[0] <= 0)
                    openMainFirst = true;
                else if (orders.count[1] <= 0)
                    openMainFirst = true;
                else
                    openMainFirst = false;

                int nextResolution;
                int closeResolution;
                if (buyTypes.array[i])
                    nextResolution = i + 3;
                else
                    nextResolution = i + 1;

                closeResolution = i + 2;

                //Разрешение на открытие первого ордера последующих стратегий
                if (i > 1)
                    if (strategySequence.array[i])
                        openMainFirst = true;
                    else
                        openMainFirst = false;

                var preArray = levelDictionary[i];
                double pipstepCoefficient = preArray[0];
                double lotCoefficient = preArray[1];

                //Открытие первого ордера
                if (openMainFirst)
                    if (orders.count[i] <= 0 && strategySequence.array[i])
                        if (buyTypes.array[i])
                            ticket = OrderOpenUp(i, pipstepCoefficient, lotCoefficient, i, closeResolution, setup);
                        else
                            ticket = OrderOpenDown(i, pipstepCoefficient, lotCoefficient, i, closeResolution, setup);

                //Открытие последующих ордеров
                if (orders.count[i] > 0)
                    if (orders.count[i] < setup.ordersCount)
                    {
                        int ask = setup.marketAsk;
                        int bid = setup.marketBid;

                        if (buyTypes.array[i])
                        {
                            if (ask <= items.array[i])
                            {
                                ticket = OrderOpenUp(i, pipstepCoefficient, lotCoefficient, 0, 0, setup);
                                if (orders.count[i] == 2)
                                    strategySequence.array[nextResolution] = true;
                            }
                        }
                        else if (!buyTypes.array[i])
                            if (bid >= items.array[i])
                            {
                                ticket = OrderOpenDown(i, pipstepCoefficient, lotCoefficient, 0, 0, setup);
                                if (orders.count[i] == 2)
                                    strategySequence.array[nextResolution] = true;
                            }
                    }
            }

            return ticket; //Только для C#
        }

        private bool SelectOrder(int index, bool positionType, bool historyType)
        {
            Dictionary<int, Order> dictionary;
            if (historyType)
                dictionary = closedOrdersDic;
            else
                dictionary = openedOrdersDic;

            if (positionType)
            {
                selectedOrder = dictionary.ElementAt(index).Value;
                return true;
            }
            else if (dictionary.ContainsKey(index))
            {
                selectedOrder = dictionary[index];
                return true;
            }
            else
                return false;
        }

        private void OrdersCheckClear(short index)
        {
            for (int i = 0; i < orders.count[index]; i++)
            {
                if (!SelectOrder(orders.array[index, i], false, false))
                {
                    orders.RemoveAt(index, i);
                    if (buyTypes.array[i])
                        items.array[index] = int.MaxValue;
                    else
                        items.array[index] = int.MinValue;
                    i--;
                }
            }
        }

        private int OrderOpenUp(short index, double pipstepCoefficient, double lotCoefficient, int resolutionIndex, int nextResolutionIndex, Setup setup)
        {
            if (!strategySequence.array[resolutionIndex])
                return setup.ticket;
            else
                strategySequence.array[nextResolutionIndex] = false;

            double lot = Math.Round(setup.lot * lotCoefficient * Math.Pow(setup.LOC, orders.count[index]), 2);

            if (Math.Round(lot, 2) < 0.01)
                lot = 0.01;

            var ask = setup.marketAsk;
            var orderID = OrderSend(OrderType.Buy, lot, ask + setup.TP, setup);

            orders.Add(index, orderID);

            items.array[index] = (int)(ask - setup.startPipstep * Math.Pow(setup.POC, orders.count[index] - 1) * pipstepCoefficient);
            if (items.array[index] == ask)
                items.array[index] -= 1;

            OrdersModify(index);

            return orderID;
        }

        private int OrderOpenDown(short index, double pipstepCoefficient, double lotCoefficient, int resolutionIndex, int nextResolutionIndex, Setup setup)
        {
            if (!strategySequence.array[resolutionIndex])
                return setup.ticket;
            else
                strategySequence.array[nextResolutionIndex] = false;

            double lot = Math.Round(setup.lot * lotCoefficient * Math.Pow(setup.LOC, orders.count[index]));

            if (Math.Round(lot, 2) < 0.01)
                lot = 0.01;

            var bid = setup.marketBid;
            var orderID = OrderSend(OrderType.Sell, lot, bid - setup.TP, setup);

            orders.Add(index, orderID);

            items.array[index] = (int)(bid + setup.startPipstep * Math.Pow(setup.POC, orders.count[index] - 1) * pipstepCoefficient);
            if (items.array[index] == bid)
                items.array[index] += 1;

            OrdersModify(index);

            return orderID;
        }

        private void OrdersModify(int index)
        {
            if (orders.count[index] <= 0) return;

            double numerator = 0;
            double denumerator = 0;

            for (int i = 0; i < orders.count[index]; i++)
            {
                double lot = 0;
                double taikprofit = 0;

                if (SelectOrder(orders.array[index, i], false, false))
                {
                    lot = selectedOrder.lot;
                    taikprofit = selectedOrder.taikeprofitPrice;

                    //if ((lot - 0.00000001) > 0)
                    //    if ((taikprofit - 0.00000001) > 0)
                    //        break;
                }

                if (Math.Round(lot, 2) < 0.0)
                    return;

                numerator += taikprofit * lot;
                denumerator += lot;
            }

            int newTaikprofit = (int)Math.Round(numerator / denumerator, 0);

            for (int i = 0; i < orders.count[index]; i++)
            {
                if (!SelectOrder(orders.array[index, i], false, false))
                {
                    if (!SelectOrder(selectedOrder.ticket, false, true))
                        OrderModify(selectedOrder.ticket, newTaikprofit);
                }
                else
                    OrderModify(selectedOrder.ticket, newTaikprofit);
            }
        }

        public Result[] SimulateMathod(Setup setup, Tick[] tickList)
        {
            double profit = 0;
            double mass = 0;
            double margin = 0;
            double marginMax = double.MinValue;
            double massMin = double.MaxValue;
            double levelMin = double.MaxValue;
            int openedCount = 0;
            int closedCount = 0;
            Order order;
            Result result1 = new Result();
            Result result2;
            double opt = 100 * setup.coefficientFirst;
            setup.marketBid = tickList[time].buy;
            setup.marketAsk = tickList[time].sell;

            var newTicket = OnInit(setup);

            if (newTicket != -1)
                setup.ticket = newTicket;

            while (time < tickList.Length)
            {
                double marginTemporary = 0;
                double massTemporary = 0;

                setup.marketBid = tickList[time].buy;
                setup.marketAsk = tickList[time].sell;

                for (var i = 0; i < openedOrdersList.count[0]; i++)
                {
                    order = openedOrdersList.array[i];

                    if (order.type == OrderType.Buy)
                    {
                        marginTemporary += order.lot * opt;

                        var profitBuy = order.GetProfitBuy(setup.marketBid, itemReversForGetProfit);
                        massTemporary += profitBuy;

                        if (order.taikeprofitPrice <= setup.marketBid)
                        {
                            order.CloseOrder(setup.marketBid, openedOrdersList, closedOrdersList, openedOrdersDic, closedOrdersDic, this.time);
                            i--;
                            profit += profitBuy;
                        }
                    }
                    else
                    {
                        marginTemporary -= order.lot * opt;

                        var profitSell = order.GetProfitSell(setup.marketAsk, itemReversForGetProfit);
                        massTemporary += profitSell;

                        if (order.taikeprofitPrice >= setup.marketAsk)
                        {
                            order.CloseOrder(setup.marketAsk, openedOrdersList, closedOrdersList, openedOrdersDic, closedOrdersDic, this.time);
                            i--;
                            profit += profitSell;
                        }
                    }
                }

                double marginAbs = Math.Abs(marginTemporary);

                if (marginMax < marginAbs)
                    marginMax = marginAbs;

                if (massMin > massTemporary)
                    massMin = massTemporary;

                if (marginAbs <= 0)
                    marginAbs = 0.1;

                var levelNew = (setup.balance + profit + massTemporary) / marginAbs;
                if (massTemporary != 0.0)
                    if (levelMin > levelNew)
                        levelMin = levelNew;

                if (time == tickList.Length / 2)
                {
                    foreach (var o in openedOrdersList.array)
                    {
                        if (o.type == OrderType.Buy)
                        {
                            margin += o.lot * opt;
                            mass += o.GetProfitBuy(setup.marketBid, itemReversForGetProfit);
                        }
                        else
                        {
                            margin -= o.lot * opt;
                            mass += o.GetProfitSell(setup.marketAsk, itemReversForGetProfit);
                        }

                        openedCount++;
                    }

                    if (margin <= 0)
                        margin = 0.1;

                    foreach (var o in closedOrdersList.array)
                        closedCount++;


                    result1 = new Result()
                    {
                        profit = profit,
                        mass = mass,
                        margin = margin,
                        marginMax = marginMax,
                        massMin = massMin,
                        levelMin = Math.Round(levelMin, 2) * 100,
                        ordersOpenedCount1 = openedCount,
                        ordersClosedCount1 = closedCount
                    };

                    mass = 0;
                    margin = 0;
                    openedCount = 0;
                    closedCount = 0;
                }

                newTicket = OnTick(setup);

                if (newTicket != -1)
                    setup.ticket = newTicket;
            }

            foreach (var o in openedOrdersList.array)
            {
                if (o.type == OrderType.Buy)
                {
                    margin += o.lot * opt;
                    mass += o.GetProfitBuy(setup.marketBid, itemReversForGetProfit);
                }
                else
                {
                    margin -= o.lot * opt;
                    mass += o.GetProfitSell(setup.marketAsk, itemReversForGetProfit);
                }

                openedCount++;
            }

            if (margin <= 0)
                margin = 0.1;

            foreach (var o in closedOrdersList.array)
                closedCount++;

            result2 = new Result()
            {
                profit = profit - result1.profit,
                mass = mass - result1.mass,
                margin = margin,
                marginMax = marginMax,
                massMin = massMin,
                levelMin = Math.Round(levelMin, 2) * 100,
                ordersOpenedCount2 = openedCount,
                ordersClosedCount2 = closedCount - result1.ordersClosedCount1
            };

            return new Result[] { result1, result2 };
        }
    }
}





public static class Extentions
{
    public static int ToInt(this string input)
    {
        string result;
        if (input.Contains(","))
        {
            var temporary = input.Split(',');
            result = temporary[0] + "." + temporary[1];
        }
        else
            result = input;
        return int.Parse(result, CultureInfo.InvariantCulture);
    }

    public static short ToShort(this string input)
    {
        string result;
        if (input.Contains(","))
        {
            var temporary = input.Split(',');
            result = temporary[0] + "." + temporary[1];
        }
        else
            result = input;
        return short.Parse(result, CultureInfo.InvariantCulture);
    }

    public static byte ToByte(this string input)
    {
        string result;
        if (input.Contains(","))
        {
            var temporary = input.Split(',');
            result = temporary[0] + "." + temporary[1];
        }
        else
            result = input;
        return byte.Parse(result, CultureInfo.InvariantCulture);
    }

    public static double ToDouble(this string input)
    {
        string result;
        if (input.Contains(","))
        {
            var temporary = input.Split(',');
            result = temporary[0] + "." + temporary[1];
        }
        else
            result = input;
        return double.Parse(result, CultureInfo.InvariantCulture);
    }

    public static float ToFloat(this string input)
    {
        string result;
        if (input.Contains(","))
        {
            var temporary = input.Split(',');
            result = temporary[0] + "." + temporary[1];
        }
        else
            result = input;
        return float.Parse(result, CultureInfo.InvariantCulture);
    }

    public static bool Compare(this double a, double b, string type)
    {
        switch (type)
        {
            case "==": return Math.Abs(a - b) <= double.Epsilon;
            case "!=": return Math.Abs(a - b) > double.Epsilon;
            case "<": return a < b - double.Epsilon;
            case "<=": return a <= b + double.Epsilon;
            case ">=": return a >= b - double.Epsilon;
            case ">": return a > b + double.Epsilon;
            default: throw new InvalidDataException();
        }
    }

    public static bool Compare(this float a, float b, string type)
    {
        switch (type)
        {
            case "==": return Math.Abs(a - b) <= float.Epsilon;
            case "!=": return Math.Abs(a - b) > float.Epsilon;
            case "<": return a < b - float.Epsilon;
            case "<=": return a <= b + float.Epsilon;
            case ">=": return a >= b - float.Epsilon;
            case ">": return a > b + float.Epsilon;
            default: throw new InvalidDataException();
        }
    }
}

public enum OrderType
{
    Buy,
    Sell
}

public struct Tick
{
    public int time;
    public int sell;
    public int buy;
}

public struct ArrayList<T>
{
    public short[] count;
    public T[] array;

    public ArrayList(short arraySize)
    {
        count = new short[1];
        array = new T[arraySize];
    }

    public void Add(T value)
    {
        array[count[0]] = value;
        count[0]++;
    }

    public void RemoveAt(short index)
    {
        for (var i = index; i < count[0] - 1; i++)
            array[i] = array[i + 1];
        count[0]--;
    }

    public void Clear(T value)
    {
        for (var i = 0; i < array.Length; i++)
            array[i] = value;
        count[0] = 0;
    }
}

public class ArrayListC<T>
{
    public short[] count;
    public List<T> array;

    public ArrayListC()
    {
        count = new short[1];
        array = new List<T>();
    }

    public void Add(T value)
    {
        array.Add(value);
        count[0]++;
    }

    public void RemoveAt(short index)
    {
        array.RemoveAt(index);
        count[0]--;
    }

    public void Clear()
    {
        array.Clear();
        count[0] = 0;
    }
}

public struct ArrayListList<T>
{
    public short[] count;
    public T[,] array;

    public ArrayListList(short strategySize, short ordersSize)
    {
        count = new short[strategySize];
        array = new T[strategySize, ordersSize];
    }

    public void Add(short index, T value)
    {
        array[index, count[index]] = value;
        count[index]++;
    }

    public void RemoveAt(short index1, int index2)
    {
        for (var i = index2; i < count[index1] - 1; i++)
            array[index1, i] = array[index1, i + 1];
        count[index1]--;
    }

    public void Clear(T value)
    {
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
                array[i, j] = value;
            count[i] = 0;
        }
    }
}

public struct Order
{
    public int ticket;
    public int openTime;
    public int closeTime;
    public OrderType type;
    public double lot;
    public int openPrice;
    public int taikeprofitPrice;
    public int lastPrise;

    public bool Compare(Order o2)
    {
        if (ticket == o2.ticket)
            return true;

        return false;
    }

    public void CloseOrder(int closePrice, ArrayListC<Order> openedOrdersList, ArrayListC<Order> closedOrdersList, Dictionary<int, Order> openedOrdersDic, Dictionary<int, Order> closedOrdersDic, int time)
    {
        lastPrise = closePrice;
        short i;
        for (i = 0; i < openedOrdersList.count[0]; i++)
            if (openedOrdersList.array[i].Compare(this))
            {
                closeTime = time;

                closedOrdersList.Add(this);
                openedOrdersList.RemoveAt(i);

                closedOrdersDic.Add(this.ticket, this);
                openedOrdersDic.Remove(this.ticket);

                return;
            }

        if (i >= openedOrdersList.count[0])
            throw new Exception();
    }

    public double GetProfitBuy(int closePrice, double itemReversForGetProfit)
    {
        lastPrise = closePrice;
        return lot * (lastPrise - openPrice) * itemReversForGetProfit;
    }

    public double GetProfitSell(int closePrice, double itemReversForGetProfit)
    {
        lastPrise = closePrice;
        return lot * (openPrice - lastPrise) * itemReversForGetProfit;
    }
}

public struct Setup
{
    public double lot;
    public double LOC;
    public double LSC;
    public int TP;
    public int strategyCount;
    public int startPipstep;
    public int ordersCount;
    public double POC;
    public double PSC;
    public double balance;
    public int accurasity;
    public int ticket;
    public int marketAsk;
    public int marketBid;
    public double coefficientFirst;
    public double coefficientSecond;
    public int selectedOrder;
    public ArrayList<int> items;
    public ArrayList<bool> buyTypes;
    public ArrayList<bool> timers;
    public ArrayList<bool> strategySequence;
    public ArrayListList<int> orders;
    public ArrayListC<Order> openedOrders;
    public ArrayListC<Order> closedOrders;

    public Setup(double lot, double LOC, double LSC, int TP, int strategyCount, int ordersCount, int startPipstep, double POC, double PSC, double balance, int accurasity, double coefficientFirst, double coefficientSecond)
    {
        this.lot = Math.Round(lot, 2);
        this.LOC = LOC;
        this.LSC = LSC;
        this.TP = TP;
        this.strategyCount = strategyCount;
        this.startPipstep = startPipstep;
        this.ordersCount = ordersCount;
        this.POC = POC;
        this.PSC = PSC;
        this.balance = balance;

        this.accurasity = accurasity;
        this.coefficientFirst = coefficientFirst;
        this.coefficientSecond = coefficientSecond;
        this.ticket = 1;
        this.marketAsk = -1;
        this.marketBid = -1;
        this.selectedOrder = -1;

        //this.nextTime = new ArrayList<int>((short)strategyCount);
        this.items = new ArrayList<int>((short)strategyCount);
        this.buyTypes = new ArrayList<bool>((short)strategyCount);
        this.timers = new ArrayList<bool>((short)strategyCount);
        this.strategySequence = new ArrayList<bool>((short)(strategyCount + 3));
        this.orders = new ArrayListList<int>((short)strategyCount, (short)ordersCount);
        this.openedOrders = new ArrayListC<Order>();
        this.closedOrders = new ArrayListC<Order>();

        openedOrders.Clear();
        closedOrders.Clear();
        items.Clear(0);
        timers.Clear(false);
        buyTypes.Clear(false);
        strategySequence.Clear(false);
        orders.Clear(0);
    }

    public Setup Copy()
    {
        return new Setup(
            Math.Round(lot, 2),
            Math.Round(LOC, 2),
            Math.Round(LSC, 2), 
            TP, 
            strategyCount, 
            ordersCount, 
            startPipstep,
            Math.Round(POC, 2),
            Math.Round(PSC, 2),
            Math.Round(balance, 4), 
            accurasity,
            Math.Round(coefficientFirst, 4),
            Math.Round(coefficientSecond, 4)
            );
    }

    public new bool Equals(object x, object y)
    {
        var X = (Setup)x;
        var Y = (Setup)y;

        if (X.accurasity != Y.accurasity) return false;
        if (X.strategyCount != Y.strategyCount) return false;
        if (X.ordersCount != Y.ordersCount) return false;
        if (X.startPipstep != Y.startPipstep) return false;
        if (X.TP != Y.TP) return false;

        if (Math.Round(X.lot, 2).Compare(Math.Round(Y.lot, 2), "!=")) return false;
        if (Math.Round(X.LOC, 2).Compare(Math.Round(Y.LOC, 2), "!=")) return false;
        if (Math.Round(X.POC, 2).Compare(Math.Round(Y.POC, 2), "!=")) return false;
        if (Math.Round(X.LSC, 2).Compare(Math.Round(Y.LSC, 2), "!=")) return false;
        if (Math.Round(X.PSC, 2).Compare(Math.Round(Y.PSC, 2), "!=")) return false;
        if (Math.Round(X.balance, 4).Compare(Math.Round(Y.balance, 4), "!=")) return false;
        if (Math.Round(X.coefficientFirst, 4).Compare(Math.Round(Y.coefficientFirst, 4), "!=")) return false;
        if (Math.Round(X.coefficientSecond, 4).Compare(Math.Round(Y.coefficientSecond, 4), "!=")) return false;

        lot = Math.Round(lot, 2);
        LOC = Math.Round(LOC, 2);
        LSC = Math.Round(LSC, 2);
        POC = Math.Round(POC, 2);
        PSC = Math.Round(PSC, 2);
        balance = Math.Round(balance, 4);
        coefficientFirst = Math.Round(coefficientFirst, 4);
        coefficientSecond = Math.Round(coefficientSecond, 4);

        return true;
    }

    public int HashCode
    {
        get
        {
            var code = accurasity.ToString() +
            strategyCount.ToString() +
            ordersCount.ToString() +
            balance.ToString() +
            coefficientFirst.ToString() +
            coefficientSecond.ToString() +
            lot.ToString() +
            LOC.ToString() +
            POC.ToString() +
            LSC.ToString() +
            PSC.ToString() +
            TP.ToString() +
            startPipstep.ToString() +
            ticket.ToString() +
            marketAsk.ToString() +
            marketBid.ToString() +
            selectedOrder.ToString();

            return code.GetHashCode();
        }
    }

    public override string ToString()
    {
        return
            Math.Round(lot, 2).ToString() + "\t" +
            Math.Round(LOC, 2).ToString() + "\t" +
            Math.Round(LSC, 2).ToString() + "\t" +
            Math.Round(POC, 2).ToString() + "\t" +
            Math.Round(PSC, 2).ToString() + "\t" +
            TP.ToString() + "\t" +
            startPipstep.ToString() + "\t" +
            Math.Round(coefficientFirst, 4).ToString() + "\t" +
            Math.Round(coefficientSecond, 4).ToString() + "\t" +
            strategyCount.ToString() + "\t" +
            ordersCount.ToString() + "\t" +
            Math.Round(balance, 4).ToString() + "\t" +
            accurasity.ToString();
    }
}

public struct Result
{
    public double profit;
    public double mass;
    public double massMin;
    public double margin;
    public double marginMax;
    public double levelMin;
    public double coefficient;
    public int ordersOpenedCount1;
    public int ordersClosedCount1;
    public int ordersOpenedCount2;
    public int ordersClosedCount2;

    public Result(ResultClass resultTemporary)
    {
        margin = Math.Round(resultTemporary.margin, 4);
        marginMax = Math.Round(resultTemporary.marginMax, 4);
        levelMin = Math.Round(resultTemporary.levelMin, 4);
        mass = Math.Round(resultTemporary.mass, 4);
        profit = Math.Round(resultTemporary.profit, 4);
        massMin = Math.Round(resultTemporary.massMin, 4);
        coefficient = Math.Round(resultTemporary.coefficient, 4);
        ordersOpenedCount1 = resultTemporary.ordersOpenedCount2;
        ordersClosedCount1 = resultTemporary.ordersClosedCount2;
        ordersOpenedCount2 = resultTemporary.ordersOpenedCount2;
        ordersClosedCount2 = resultTemporary.ordersClosedCount2;
    }

    public Result(int i)
    {
        margin = i;
        marginMax = i;
        levelMin = i;
        mass = i;
        profit = i;
        massMin = i;
        coefficient = i;
        ordersOpenedCount1 = i;
        ordersClosedCount1 = i;
        ordersOpenedCount2 = i;
        ordersClosedCount2 = i;
    }
}

public class ResultClass
{
    public double profit;
    public double mass;
    public double massMin;
    public double margin;
    public double marginMax;
    public double levelMin;
    public double coefficient;
    public int ordersOpenedCount1;
    public int ordersClosedCount1;
    public int ordersOpenedCount2;
    public int ordersClosedCount2;

    public ResultClass(Result resultTemporary)
    {
        margin = Math.Round(resultTemporary.margin, 4);
        marginMax = Math.Round(resultTemporary.marginMax, 4);
        levelMin = Math.Round(resultTemporary.levelMin, 4);
        mass = Math.Round(resultTemporary.mass, 4);
        massMin= Math.Round(resultTemporary.massMin, 4);
        profit = Math.Round(resultTemporary.profit, 4);
        massMin = Math.Round(resultTemporary.massMin, 4);
        ordersOpenedCount1 = resultTemporary.ordersOpenedCount1;
        ordersClosedCount1 = resultTemporary.ordersClosedCount1;
        ordersOpenedCount2 = resultTemporary.ordersOpenedCount2;
        ordersClosedCount2 = resultTemporary.ordersClosedCount2;
    }

    public ResultClass()
    {

    }

    public new bool Equals(object x, object y)
    {
        var X = (ResultClass)x;
        var Y = (ResultClass)y;

        if (X.ordersOpenedCount1 != Y.ordersOpenedCount1) return false;
        if (X.ordersOpenedCount2 != Y.ordersOpenedCount2) return false;
        if (X.ordersClosedCount1 != Y.ordersClosedCount1) return false;
        if (X.ordersClosedCount2 != Y.ordersClosedCount2) return false;

        if (Math.Round(X.coefficient, 4).Compare(Math.Round(Y.coefficient, 4), "!=")) return false;
        if (Math.Round(X.levelMin, 4).Compare(Math.Round(Y.levelMin, 4), "!=")) return false;
        if (Math.Round(X.margin, 4).Compare(Math.Round(Y.margin, 4), "!=")) return false;
        if (Math.Round(X.marginMax, 4).Compare(Math.Round(Y.marginMax, 4), "!=")) return false;
        if (Math.Round(X.mass, 4).Compare(Math.Round(Y.mass, 4), "!=")) return false;
        if (Math.Round(X.massMin, 4).Compare(Math.Round(Y.massMin, 4), "!=")) return false;
        if (Math.Round(X.profit, 4).Compare(Math.Round(Y.profit, 4), "!=")) return false;

        margin = Math.Round(X.margin, 4);
        marginMax = Math.Round(X.marginMax, 4);
        levelMin = Math.Round(X.levelMin, 4);
        mass = Math.Round(X.mass, 4);
        profit = Math.Round(X.profit, 4);
        massMin = Math.Round(X.massMin, 4);
        coefficient = Math.Round(X.coefficient, 4);
        ordersOpenedCount1 = X.ordersOpenedCount2;
        ordersClosedCount1 = X.ordersClosedCount2;
        ordersOpenedCount2 = X.ordersOpenedCount2;
        ordersClosedCount2 = X.ordersClosedCount2;

        return true;
    }
}

public class LevelIteration
{
    public double Lot { get; private set; }
    public double Level { get; private set; }

    public LevelIteration(Setup setup, List<Tick> tickList, Func<Setup, List<Tick>, double, double> function, double lot)
    {
        Lot = lot;
        Level = function(setup.Copy(), tickList, lot);
    }

    public LevelIteration(double lot, double level)
    {
        Lot = lot;
        Level = level;
    }
}