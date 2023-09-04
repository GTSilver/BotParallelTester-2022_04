
namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Calculate_B = new System.Windows.Forms.Button();
            this.Lot_TB = new System.Windows.Forms.TextBox();
            this.StrategyCount_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.OrdersCount_TB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.GraphicPath_TB = new System.Windows.Forms.TextBox();
            this.GraphicOpen_B = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.Accurasity_TB = new System.Windows.Forms.TextBox();
            this.PSCMin_TB = new System.Windows.Forms.TextBox();
            this.POCMin_TB = new System.Windows.Forms.TextBox();
            this.StartPipstepMin_TB = new System.Windows.Forms.TextBox();
            this.TakeprofitMin_TB = new System.Windows.Forms.TextBox();
            this.LSCMin_TB = new System.Windows.Forms.TextBox();
            this.LOCMin_TB = new System.Windows.Forms.TextBox();
            this.PSCMax_TB = new System.Windows.Forms.TextBox();
            this.POCMax_TB = new System.Windows.Forms.TextBox();
            this.StartPipstepMax_TB = new System.Windows.Forms.TextBox();
            this.TakeprofitMax_TB = new System.Windows.Forms.TextBox();
            this.LSCMax_TB = new System.Windows.Forms.TextBox();
            this.LOCMax_TB = new System.Windows.Forms.TextBox();
            this.PSCStep_TB = new System.Windows.Forms.TextBox();
            this.POCStep_TB = new System.Windows.Forms.TextBox();
            this.StartPipstepStep_TB = new System.Windows.Forms.TextBox();
            this.TakeprofitStep_TB = new System.Windows.Forms.TextBox();
            this.LSCStep_TB = new System.Windows.Forms.TextBox();
            this.LOCStep_TB = new System.Windows.Forms.TextBox();
            this.Stop_B = new System.Windows.Forms.Button();
            this.MarjenLimit_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CoefficientFirst_TB = new System.Windows.Forms.TextBox();
            this.CoefficientSecond_TB = new System.Windows.Forms.TextBox();
            this.ClearProfitFilter_CB = new System.Windows.Forms.CheckBox();
            this.Progress_PB = new System.Windows.Forms.ProgressBar();
            this.IterationCount_L = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.Balance_TB = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.AlgorithmType_CB = new System.Windows.Forms.CheckBox();
            this.SetupSave_B = new System.Windows.Forms.Button();
            this.SetupLoad_B = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.ResultOpen_B = new System.Windows.Forms.Button();
            this.ResultPath_TB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.CoefficientFilter_CB = new System.Windows.Forms.CheckBox();
            this.CoefficientLimit_TB = new System.Windows.Forms.TextBox();
            this.LevelFilter_CB = new System.Windows.Forms.CheckBox();
            this.LevelLimit_TB = new System.Windows.Forms.TextBox();
            this.Pause_B = new System.Windows.Forms.Button();
            this.Continue_B = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.TestDroprate_TB = new System.Windows.Forms.TextBox();
            this.Test_B = new System.Windows.Forms.Button();
            this.TestCount_TB = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.VriantsAll_CB = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.LevelType_RB = new System.Windows.Forms.RadioButton();
            this.Frow_B = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // Calculate_B
            // 
            this.Calculate_B.Location = new System.Drawing.Point(457, 223);
            this.Calculate_B.Name = "Calculate_B";
            this.Calculate_B.Size = new System.Drawing.Size(141, 22);
            this.Calculate_B.TabIndex = 0;
            this.Calculate_B.Text = "Начать расчёт";
            this.Calculate_B.UseVisualStyleBackColor = true;
            this.Calculate_B.Click += new System.EventHandler(this.Calculate_B_Click);
            // 
            // Lot_TB
            // 
            this.Lot_TB.Location = new System.Drawing.Point(352, 32);
            this.Lot_TB.Name = "Lot_TB";
            this.Lot_TB.Size = new System.Drawing.Size(102, 20);
            this.Lot_TB.TabIndex = 4;
            this.Lot_TB.Text = "0.75";
            this.Lot_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StrategyCount_TB
            // 
            this.StrategyCount_TB.Location = new System.Drawing.Point(352, 58);
            this.StrategyCount_TB.Name = "StrategyCount_TB";
            this.StrategyCount_TB.Size = new System.Drawing.Size(102, 20);
            this.StrategyCount_TB.TabIndex = 8;
            this.StrategyCount_TB.Text = "6";
            this.StrategyCount_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Начальный лот";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Коэф. лота ордеров";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Коэф. лота стратегий";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Тейкпрофит";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(247, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Кол-во стратегий";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Начальный пипстеп";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(247, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Кол-во ордеров";
            // 
            // OrdersCount_TB
            // 
            this.OrdersCount_TB.Location = new System.Drawing.Point(352, 84);
            this.OrdersCount_TB.Name = "OrdersCount_TB";
            this.OrdersCount_TB.Size = new System.Drawing.Size(102, 20);
            this.OrdersCount_TB.TabIndex = 16;
            this.OrdersCount_TB.Text = "10";
            this.OrdersCount_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Коэф. пипст. ордер.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Коэф. пипст. страт.";
            // 
            // GraphicPath_TB
            // 
            this.GraphicPath_TB.Location = new System.Drawing.Point(171, 196);
            this.GraphicPath_TB.Name = "GraphicPath_TB";
            this.GraphicPath_TB.Size = new System.Drawing.Size(427, 20);
            this.GraphicPath_TB.TabIndex = 35;
            this.GraphicPath_TB.Text = "EURUSD 20-12-2021=-23-02-2022.txt";
            this.GraphicPath_TB.TextChanged += new System.EventHandler(this.GraphicPath_TB_TextChanged);
            // 
            // GraphicOpen_B
            // 
            this.GraphicOpen_B.Location = new System.Drawing.Point(604, 195);
            this.GraphicOpen_B.Name = "GraphicOpen_B";
            this.GraphicOpen_B.Size = new System.Drawing.Size(69, 22);
            this.GraphicOpen_B.TabIndex = 36;
            this.GraphicOpen_B.Text = "Открыть";
            this.GraphicOpen_B.UseVisualStyleBackColor = true;
            this.GraphicOpen_B.Click += new System.EventHandler(this.Open_B_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(247, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "Точность символа";
            // 
            // Accurasity_TB
            // 
            this.Accurasity_TB.Location = new System.Drawing.Point(352, 110);
            this.Accurasity_TB.Name = "Accurasity_TB";
            this.Accurasity_TB.Size = new System.Drawing.Size(102, 20);
            this.Accurasity_TB.TabIndex = 37;
            this.Accurasity_TB.Text = "4";
            this.Accurasity_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PSCMin_TB
            // 
            this.PSCMin_TB.Location = new System.Drawing.Point(133, 84);
            this.PSCMin_TB.Name = "PSCMin_TB";
            this.PSCMin_TB.Size = new System.Drawing.Size(32, 20);
            this.PSCMin_TB.TabIndex = 48;
            this.PSCMin_TB.Text = "0.6";
            this.PSCMin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // POCMin_TB
            // 
            this.POCMin_TB.Location = new System.Drawing.Point(133, 58);
            this.POCMin_TB.Name = "POCMin_TB";
            this.POCMin_TB.Size = new System.Drawing.Size(32, 20);
            this.POCMin_TB.TabIndex = 47;
            this.POCMin_TB.Text = "0.6";
            this.POCMin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartPipstepMin_TB
            // 
            this.StartPipstepMin_TB.Location = new System.Drawing.Point(133, 136);
            this.StartPipstepMin_TB.Name = "StartPipstepMin_TB";
            this.StartPipstepMin_TB.Size = new System.Drawing.Size(32, 20);
            this.StartPipstepMin_TB.TabIndex = 45;
            this.StartPipstepMin_TB.Text = "1";
            this.StartPipstepMin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TakeprofitMin_TB
            // 
            this.TakeprofitMin_TB.Location = new System.Drawing.Point(133, 110);
            this.TakeprofitMin_TB.Name = "TakeprofitMin_TB";
            this.TakeprofitMin_TB.Size = new System.Drawing.Size(32, 20);
            this.TakeprofitMin_TB.TabIndex = 43;
            this.TakeprofitMin_TB.Text = "1";
            this.TakeprofitMin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LSCMin_TB
            // 
            this.LSCMin_TB.Location = new System.Drawing.Point(133, 32);
            this.LSCMin_TB.Name = "LSCMin_TB";
            this.LSCMin_TB.Size = new System.Drawing.Size(32, 20);
            this.LSCMin_TB.TabIndex = 42;
            this.LSCMin_TB.Text = "0.6";
            this.LSCMin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LOCMin_TB
            // 
            this.LOCMin_TB.Location = new System.Drawing.Point(133, 6);
            this.LOCMin_TB.Name = "LOCMin_TB";
            this.LOCMin_TB.Size = new System.Drawing.Size(32, 20);
            this.LOCMin_TB.TabIndex = 41;
            this.LOCMin_TB.Text = "0.6";
            this.LOCMin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PSCMax_TB
            // 
            this.PSCMax_TB.Location = new System.Drawing.Point(171, 84);
            this.PSCMax_TB.Name = "PSCMax_TB";
            this.PSCMax_TB.Size = new System.Drawing.Size(32, 20);
            this.PSCMax_TB.TabIndex = 58;
            this.PSCMax_TB.Text = "3";
            this.PSCMax_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // POCMax_TB
            // 
            this.POCMax_TB.Location = new System.Drawing.Point(171, 58);
            this.POCMax_TB.Name = "POCMax_TB";
            this.POCMax_TB.Size = new System.Drawing.Size(32, 20);
            this.POCMax_TB.TabIndex = 57;
            this.POCMax_TB.Text = "3";
            this.POCMax_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartPipstepMax_TB
            // 
            this.StartPipstepMax_TB.Location = new System.Drawing.Point(171, 136);
            this.StartPipstepMax_TB.Name = "StartPipstepMax_TB";
            this.StartPipstepMax_TB.Size = new System.Drawing.Size(32, 20);
            this.StartPipstepMax_TB.TabIndex = 55;
            this.StartPipstepMax_TB.Text = "50";
            this.StartPipstepMax_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TakeprofitMax_TB
            // 
            this.TakeprofitMax_TB.Location = new System.Drawing.Point(171, 110);
            this.TakeprofitMax_TB.Name = "TakeprofitMax_TB";
            this.TakeprofitMax_TB.Size = new System.Drawing.Size(32, 20);
            this.TakeprofitMax_TB.TabIndex = 53;
            this.TakeprofitMax_TB.Text = "50";
            this.TakeprofitMax_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LSCMax_TB
            // 
            this.LSCMax_TB.Location = new System.Drawing.Point(171, 32);
            this.LSCMax_TB.Name = "LSCMax_TB";
            this.LSCMax_TB.Size = new System.Drawing.Size(32, 20);
            this.LSCMax_TB.TabIndex = 52;
            this.LSCMax_TB.Text = "3";
            this.LSCMax_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LOCMax_TB
            // 
            this.LOCMax_TB.Location = new System.Drawing.Point(171, 6);
            this.LOCMax_TB.Name = "LOCMax_TB";
            this.LOCMax_TB.Size = new System.Drawing.Size(32, 20);
            this.LOCMax_TB.TabIndex = 51;
            this.LOCMax_TB.Text = "3";
            this.LOCMax_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PSCStep_TB
            // 
            this.PSCStep_TB.Location = new System.Drawing.Point(209, 84);
            this.PSCStep_TB.Name = "PSCStep_TB";
            this.PSCStep_TB.Size = new System.Drawing.Size(32, 20);
            this.PSCStep_TB.TabIndex = 68;
            this.PSCStep_TB.Text = "0.1";
            this.PSCStep_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // POCStep_TB
            // 
            this.POCStep_TB.Location = new System.Drawing.Point(209, 58);
            this.POCStep_TB.Name = "POCStep_TB";
            this.POCStep_TB.Size = new System.Drawing.Size(32, 20);
            this.POCStep_TB.TabIndex = 67;
            this.POCStep_TB.Text = "0.1";
            this.POCStep_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StartPipstepStep_TB
            // 
            this.StartPipstepStep_TB.Location = new System.Drawing.Point(209, 136);
            this.StartPipstepStep_TB.Name = "StartPipstepStep_TB";
            this.StartPipstepStep_TB.Size = new System.Drawing.Size(32, 20);
            this.StartPipstepStep_TB.TabIndex = 65;
            this.StartPipstepStep_TB.Text = "1";
            this.StartPipstepStep_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TakeprofitStep_TB
            // 
            this.TakeprofitStep_TB.Location = new System.Drawing.Point(209, 110);
            this.TakeprofitStep_TB.Name = "TakeprofitStep_TB";
            this.TakeprofitStep_TB.Size = new System.Drawing.Size(32, 20);
            this.TakeprofitStep_TB.TabIndex = 63;
            this.TakeprofitStep_TB.Text = "1";
            this.TakeprofitStep_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LSCStep_TB
            // 
            this.LSCStep_TB.Location = new System.Drawing.Point(209, 32);
            this.LSCStep_TB.Name = "LSCStep_TB";
            this.LSCStep_TB.Size = new System.Drawing.Size(32, 20);
            this.LSCStep_TB.TabIndex = 62;
            this.LSCStep_TB.Text = "0.1";
            this.LSCStep_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LOCStep_TB
            // 
            this.LOCStep_TB.Location = new System.Drawing.Point(209, 6);
            this.LOCStep_TB.Name = "LOCStep_TB";
            this.LOCStep_TB.Size = new System.Drawing.Size(32, 20);
            this.LOCStep_TB.TabIndex = 61;
            this.LOCStep_TB.Text = "0.1";
            this.LOCStep_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Stop_B
            // 
            this.Stop_B.BackColor = System.Drawing.Color.Red;
            this.Stop_B.Enabled = false;
            this.Stop_B.Location = new System.Drawing.Point(604, 223);
            this.Stop_B.Name = "Stop_B";
            this.Stop_B.Size = new System.Drawing.Size(22, 22);
            this.Stop_B.TabIndex = 71;
            this.Stop_B.UseVisualStyleBackColor = false;
            this.Stop_B.Click += new System.EventHandler(this.Stop_B_Click);
            // 
            // MarjenLimit_TB
            // 
            this.MarjenLimit_TB.Location = new System.Drawing.Point(604, 58);
            this.MarjenLimit_TB.Name = "MarjenLimit_TB";
            this.MarjenLimit_TB.Size = new System.Drawing.Size(69, 20);
            this.MarjenLimit_TB.TabIndex = 73;
            this.MarjenLimit_TB.Text = "10000";
            this.MarjenLimit_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(247, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 76;
            this.label2.Text = "Котировка Б/К";
            // 
            // CoefficientFirst_TB
            // 
            this.CoefficientFirst_TB.Location = new System.Drawing.Point(352, 6);
            this.CoefficientFirst_TB.Name = "CoefficientFirst_TB";
            this.CoefficientFirst_TB.Size = new System.Drawing.Size(48, 20);
            this.CoefficientFirst_TB.TabIndex = 75;
            this.CoefficientFirst_TB.Text = "1.0798";
            this.CoefficientFirst_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CoefficientSecond_TB
            // 
            this.CoefficientSecond_TB.Location = new System.Drawing.Point(406, 6);
            this.CoefficientSecond_TB.Name = "CoefficientSecond_TB";
            this.CoefficientSecond_TB.Size = new System.Drawing.Size(48, 20);
            this.CoefficientSecond_TB.TabIndex = 77;
            this.CoefficientSecond_TB.Text = "1.0000";
            this.CoefficientSecond_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ClearProfitFilter_CB
            // 
            this.ClearProfitFilter_CB.AutoSize = true;
            this.ClearProfitFilter_CB.Checked = true;
            this.ClearProfitFilter_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ClearProfitFilter_CB.Location = new System.Drawing.Point(460, 60);
            this.ClearProfitFilter_CB.Name = "ClearProfitFilter_CB";
            this.ClearProfitFilter_CB.Size = new System.Drawing.Size(135, 17);
            this.ClearProfitFilter_CB.TabIndex = 83;
            this.ClearProfitFilter_CB.Text = "Филтр чист. прибыли";
            this.ClearProfitFilter_CB.UseVisualStyleBackColor = true;
            this.ClearProfitFilter_CB.CheckedChanged += new System.EventHandler(this.Filter_CB_CheckedChanged);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(460, 6);
            this.Progress_PB.Name = "Progress_PB";
            this.Progress_PB.Size = new System.Drawing.Size(213, 20);
            this.Progress_PB.TabIndex = 84;
            // 
            // IterationCount_L
            // 
            this.IterationCount_L.AutoSize = true;
            this.IterationCount_L.Location = new System.Drawing.Point(551, 139);
            this.IterationCount_L.Name = "IterationCount_L";
            this.IterationCount_L.Size = new System.Drawing.Size(13, 13);
            this.IterationCount_L.TabIndex = 102;
            this.IterationCount_L.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(460, 139);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 13);
            this.label18.TabIndex = 101;
            this.label18.Text = "Кол-во итераций:";
            // 
            // Balance_TB
            // 
            this.Balance_TB.Location = new System.Drawing.Point(352, 136);
            this.Balance_TB.Name = "Balance_TB";
            this.Balance_TB.Size = new System.Drawing.Size(102, 20);
            this.Balance_TB.TabIndex = 97;
            this.Balance_TB.Text = "10000";
            this.Balance_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(247, 139);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 13);
            this.label20.TabIndex = 98;
            this.label20.Text = "Баланс счёта";
            // 
            // AlgorithmType_CB
            // 
            this.AlgorithmType_CB.AutoSize = true;
            this.AlgorithmType_CB.Enabled = false;
            this.AlgorithmType_CB.Location = new System.Drawing.Point(247, 273);
            this.AlgorithmType_CB.Name = "AlgorithmType_CB";
            this.AlgorithmType_CB.Size = new System.Drawing.Size(211, 17);
            this.AlgorithmType_CB.TabIndex = 96;
            this.AlgorithmType_CB.Text = "Использовать новый тип алгоритма";
            this.AlgorithmType_CB.UseVisualStyleBackColor = true;
            // 
            // SetupSave_B
            // 
            this.SetupSave_B.Location = new System.Drawing.Point(604, 134);
            this.SetupSave_B.Name = "SetupSave_B";
            this.SetupSave_B.Size = new System.Drawing.Size(69, 23);
            this.SetupSave_B.TabIndex = 89;
            this.SetupSave_B.Text = "Сохранить";
            this.SetupSave_B.UseVisualStyleBackColor = true;
            this.SetupSave_B.Click += new System.EventHandler(this.SetupSave_B_Click);
            // 
            // SetupLoad_B
            // 
            this.SetupLoad_B.Location = new System.Drawing.Point(604, 108);
            this.SetupLoad_B.Name = "SetupLoad_B";
            this.SetupLoad_B.Size = new System.Drawing.Size(69, 23);
            this.SetupLoad_B.TabIndex = 88;
            this.SetupLoad_B.Text = "Загрузить";
            this.SetupLoad_B.UseVisualStyleBackColor = true;
            this.SetupLoad_B.Click += new System.EventHandler(this.SetupLoad_B_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 172);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(156, 13);
            this.label16.TabIndex = 91;
            this.label16.Text = "Путь к папке с результатами";
            // 
            // ResultOpen_B
            // 
            this.ResultOpen_B.Location = new System.Drawing.Point(604, 167);
            this.ResultOpen_B.Name = "ResultOpen_B";
            this.ResultOpen_B.Size = new System.Drawing.Size(69, 22);
            this.ResultOpen_B.TabIndex = 90;
            this.ResultOpen_B.Text = "Открыть";
            this.ResultOpen_B.UseVisualStyleBackColor = true;
            this.ResultOpen_B.Click += new System.EventHandler(this.ResultOpen_B_Click);
            // 
            // ResultPath_TB
            // 
            this.ResultPath_TB.Location = new System.Drawing.Point(171, 167);
            this.ResultPath_TB.Name = "ResultPath_TB";
            this.ResultPath_TB.Size = new System.Drawing.Size(427, 20);
            this.ResultPath_TB.TabIndex = 89;
            this.ResultPath_TB.Text = "D:\\Рабочий стол";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 200);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(157, 13);
            this.label15.TabIndex = 62;
            this.label15.Text = "Путь к нормальному графику";
            // 
            // CoefficientFilter_CB
            // 
            this.CoefficientFilter_CB.AutoSize = true;
            this.CoefficientFilter_CB.Checked = true;
            this.CoefficientFilter_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CoefficientFilter_CB.Location = new System.Drawing.Point(460, 86);
            this.CoefficientFilter_CB.Name = "CoefficientFilter_CB";
            this.CoefficientFilter_CB.Size = new System.Drawing.Size(138, 17);
            this.CoefficientFilter_CB.TabIndex = 104;
            this.CoefficientFilter_CB.Text = "Филтр коэффициента";
            this.CoefficientFilter_CB.UseVisualStyleBackColor = true;
            this.CoefficientFilter_CB.CheckedChanged += new System.EventHandler(this.CoefficientFilter_CB_CheckedChanged);
            // 
            // CoefficientLimit_TB
            // 
            this.CoefficientLimit_TB.Location = new System.Drawing.Point(604, 84);
            this.CoefficientLimit_TB.Name = "CoefficientLimit_TB";
            this.CoefficientLimit_TB.Size = new System.Drawing.Size(69, 20);
            this.CoefficientLimit_TB.TabIndex = 103;
            this.CoefficientLimit_TB.Text = "0,15";
            this.CoefficientLimit_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LevelFilter_CB
            // 
            this.LevelFilter_CB.AutoSize = true;
            this.LevelFilter_CB.Checked = true;
            this.LevelFilter_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LevelFilter_CB.Location = new System.Drawing.Point(460, 34);
            this.LevelFilter_CB.Name = "LevelFilter_CB";
            this.LevelFilter_CB.Size = new System.Drawing.Size(104, 17);
            this.LevelFilter_CB.TabIndex = 106;
            this.LevelFilter_CB.Text = "Фильтр уровня";
            this.LevelFilter_CB.UseVisualStyleBackColor = true;
            this.LevelFilter_CB.CheckedChanged += new System.EventHandler(this.LevelFilter_CB_CheckedChanged);
            // 
            // LevelLimit_TB
            // 
            this.LevelLimit_TB.Location = new System.Drawing.Point(604, 32);
            this.LevelLimit_TB.Name = "LevelLimit_TB";
            this.LevelLimit_TB.Size = new System.Drawing.Size(69, 20);
            this.LevelLimit_TB.TabIndex = 105;
            this.LevelLimit_TB.Text = "200";
            this.LevelLimit_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Pause_B
            // 
            this.Pause_B.BackColor = System.Drawing.Color.Blue;
            this.Pause_B.Enabled = false;
            this.Pause_B.Location = new System.Drawing.Point(627, 223);
            this.Pause_B.Name = "Pause_B";
            this.Pause_B.Size = new System.Drawing.Size(22, 22);
            this.Pause_B.TabIndex = 107;
            this.Pause_B.UseVisualStyleBackColor = false;
            this.Pause_B.Click += new System.EventHandler(this.Pause_B_Click);
            // 
            // Continue_B
            // 
            this.Continue_B.BackColor = System.Drawing.Color.Lime;
            this.Continue_B.Enabled = false;
            this.Continue_B.Location = new System.Drawing.Point(651, 223);
            this.Continue_B.Name = "Continue_B";
            this.Continue_B.Size = new System.Drawing.Size(22, 22);
            this.Continue_B.TabIndex = 108;
            this.Continue_B.UseVisualStyleBackColor = false;
            this.Continue_B.Click += new System.EventHandler(this.Continue_B_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 228);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 114;
            this.label17.Text = "Коэф. шага лота";
            // 
            // TestDroprate_TB
            // 
            this.TestDroprate_TB.Location = new System.Drawing.Point(109, 224);
            this.TestDroprate_TB.Name = "TestDroprate_TB";
            this.TestDroprate_TB.Size = new System.Drawing.Size(32, 20);
            this.TestDroprate_TB.TabIndex = 117;
            this.TestDroprate_TB.Text = "4.5";
            this.TestDroprate_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Test_B
            // 
            this.Test_B.Location = new System.Drawing.Point(263, 223);
            this.Test_B.Name = "Test_B";
            this.Test_B.Size = new System.Drawing.Size(40, 22);
            this.Test_B.TabIndex = 118;
            this.Test_B.Text = "Тест";
            this.Test_B.UseVisualStyleBackColor = true;
            this.Test_B.Click += new System.EventHandler(this.Test_B_Click);
            // 
            // TestCount_TB
            // 
            this.TestCount_TB.Location = new System.Drawing.Point(225, 224);
            this.TestCount_TB.Name = "TestCount_TB";
            this.TestCount_TB.Size = new System.Drawing.Size(32, 20);
            this.TestCount_TB.TabIndex = 119;
            this.TestCount_TB.Text = "23";
            this.TestCount_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(147, 228);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 120;
            this.label13.Text = "Кол-во точек";
            // 
            // VriantsAll_CB
            // 
            this.VriantsAll_CB.AutoSize = true;
            this.VriantsAll_CB.Location = new System.Drawing.Point(460, 112);
            this.VriantsAll_CB.Name = "VriantsAll_CB";
            this.VriantsAll_CB.Size = new System.Drawing.Size(121, 17);
            this.VriantsAll_CB.TabIndex = 123;
            this.VriantsAll_CB.Text = "Перебор вариаций";
            this.VriantsAll_CB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.LevelType_RB);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(109, 259);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 31);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(74, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(57, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "новый";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // LevelType_RB
            // 
            this.LevelType_RB.AutoSize = true;
            this.LevelType_RB.Location = new System.Drawing.Point(6, 12);
            this.LevelType_RB.Name = "LevelType_RB";
            this.LevelType_RB.Size = new System.Drawing.Size(62, 17);
            this.LevelType_RB.TabIndex = 0;
            this.LevelType_RB.Text = "старый";
            this.LevelType_RB.UseVisualStyleBackColor = true;
            // 
            // Frow_B
            // 
            this.Frow_B.Location = new System.Drawing.Point(309, 223);
            this.Frow_B.Name = "Frow_B";
            this.Frow_B.Size = new System.Drawing.Size(142, 22);
            this.Frow_B.TabIndex = 125;
            this.Frow_B.Text = "Пересимулировать";
            this.Frow_B.UseVisualStyleBackColor = true;
            this.Frow_B.Click += new System.EventHandler(this.Frow_B_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(531, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 19);
            this.panel1.TabIndex = 126;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(36, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1, 19);
            this.panel3.TabIndex = 128;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(89, -1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1, 19);
            this.panel4.TabIndex = 127;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(89, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 19);
            this.panel2.TabIndex = 127;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Location = new System.Drawing.Point(602, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1, 19);
            this.panel5.TabIndex = 127;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Location = new System.Drawing.Point(36, -1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1, 19);
            this.panel6.TabIndex = 128;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(89, -1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1, 19);
            this.panel7.TabIndex = 127;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Location = new System.Drawing.Point(89, -1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1, 19);
            this.panel8.TabIndex = 127;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 249);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Frow_B);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.VriantsAll_CB);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.TestCount_TB);
            this.Controls.Add(this.Test_B);
            this.Controls.Add(this.TestDroprate_TB);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.Continue_B);
            this.Controls.Add(this.Pause_B);
            this.Controls.Add(this.LevelFilter_CB);
            this.Controls.Add(this.LevelLimit_TB);
            this.Controls.Add(this.CoefficientFilter_CB);
            this.Controls.Add(this.CoefficientLimit_TB);
            this.Controls.Add(this.IterationCount_L);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.Balance_TB);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.AlgorithmType_CB);
            this.Controls.Add(this.Stop_B);
            this.Controls.Add(this.GraphicPath_TB);
            this.Controls.Add(this.SetupSave_B);
            this.Controls.Add(this.SetupLoad_B);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GraphicOpen_B);
            this.Controls.Add(this.Lot_TB);
            this.Controls.Add(this.Progress_PB);
            this.Controls.Add(this.ClearProfitFilter_CB);
            this.Controls.Add(this.ResultOpen_B);
            this.Controls.Add(this.StrategyCount_TB);
            this.Controls.Add(this.Calculate_B);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ResultPath_TB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LSCMax_TB);
            this.Controls.Add(this.CoefficientSecond_TB);
            this.Controls.Add(this.LOCMax_TB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TakeprofitMax_TB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PSCMin_TB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.StartPipstepMax_TB);
            this.Controls.Add(this.CoefficientFirst_TB);
            this.Controls.Add(this.POCMin_TB);
            this.Controls.Add(this.OrdersCount_TB);
            this.Controls.Add(this.POCMax_TB);
            this.Controls.Add(this.StartPipstepMin_TB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.PSCMax_TB);
            this.Controls.Add(this.MarjenLimit_TB);
            this.Controls.Add(this.TakeprofitMin_TB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.LOCStep_TB);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.LSCMin_TB);
            this.Controls.Add(this.PSCStep_TB);
            this.Controls.Add(this.LSCStep_TB);
            this.Controls.Add(this.POCStep_TB);
            this.Controls.Add(this.LOCMin_TB);
            this.Controls.Add(this.Accurasity_TB);
            this.Controls.Add(this.TakeprofitStep_TB);
            this.Controls.Add(this.StartPipstepStep_TB);
            this.Controls.Add(this.label14);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Симуляция бота 2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Calculate_B;
        private System.Windows.Forms.TextBox Lot_TB;
        private System.Windows.Forms.TextBox StrategyCount_TB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox OrdersCount_TB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox GraphicPath_TB;
        private System.Windows.Forms.Button GraphicOpen_B;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Accurasity_TB;
        private System.Windows.Forms.TextBox PSCMin_TB;
        private System.Windows.Forms.TextBox POCMin_TB;
        private System.Windows.Forms.TextBox StartPipstepMin_TB;
        private System.Windows.Forms.TextBox TakeprofitMin_TB;
        private System.Windows.Forms.TextBox LSCMin_TB;
        private System.Windows.Forms.TextBox LOCMin_TB;
        private System.Windows.Forms.TextBox PSCMax_TB;
        private System.Windows.Forms.TextBox POCMax_TB;
        private System.Windows.Forms.TextBox StartPipstepMax_TB;
        private System.Windows.Forms.TextBox TakeprofitMax_TB;
        private System.Windows.Forms.TextBox LSCMax_TB;
        private System.Windows.Forms.TextBox LOCMax_TB;
        private System.Windows.Forms.TextBox PSCStep_TB;
        private System.Windows.Forms.TextBox POCStep_TB;
        private System.Windows.Forms.TextBox StartPipstepStep_TB;
        private System.Windows.Forms.TextBox TakeprofitStep_TB;
        private System.Windows.Forms.TextBox LSCStep_TB;
        private System.Windows.Forms.TextBox LOCStep_TB;
        private System.Windows.Forms.Button Stop_B;
        private System.Windows.Forms.TextBox MarjenLimit_TB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CoefficientFirst_TB;
        private System.Windows.Forms.TextBox CoefficientSecond_TB;
        private System.Windows.Forms.CheckBox ClearProfitFilter_CB;
        private System.Windows.Forms.ProgressBar Progress_PB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button SetupSave_B;
        private System.Windows.Forms.Button SetupLoad_B;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button ResultOpen_B;
        private System.Windows.Forms.TextBox ResultPath_TB;
        private System.Windows.Forms.CheckBox AlgorithmType_CB;
        private System.Windows.Forms.TextBox Balance_TB;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label IterationCount_L;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox CoefficientFilter_CB;
        private System.Windows.Forms.TextBox CoefficientLimit_TB;
        private System.Windows.Forms.CheckBox LevelFilter_CB;
        private System.Windows.Forms.TextBox LevelLimit_TB;
        private System.Windows.Forms.Button Pause_B;
        private System.Windows.Forms.Button Continue_B;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TestDroprate_TB;
        private System.Windows.Forms.Button Test_B;
        private System.Windows.Forms.TextBox TestCount_TB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox VriantsAll_CB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton LevelType_RB;
        private System.Windows.Forms.Button Frow_B;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
    }
}

