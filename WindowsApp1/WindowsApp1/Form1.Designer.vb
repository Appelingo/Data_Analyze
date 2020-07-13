<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.t_i = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.t_c = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Kinetic_Energy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Chart4 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart5 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(645, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "計算"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.t_i, Me.t_c, Me.Kinetic_Energy, Me.S})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 33)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(100, 97)
        Me.DataGridView1.TabIndex = 1
        '
        't_i
        '
        Me.t_i.HeaderText = "t_i"
        Me.t_i.Name = "t_i"
        '
        't_c
        '
        Me.t_c.HeaderText = "t_c"
        Me.t_c.Name = "t_c"
        '
        'Kinetic_Energy
        '
        Me.Kinetic_Energy.HeaderText = "Kinetic_Energy"
        Me.Kinetic_Energy.Name = "Kinetic_Energy"
        '
        'S
        '
        Me.S.HeaderText = "S"
        Me.S.Name = "S"
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(118, 33)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Size = New System.Drawing.Size(143, 97)
        Me.Chart1.TabIndex = 2
        Me.Chart1.Text = "Chart1"
        '
        'Chart2
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend2)
        Me.Chart2.Location = New System.Drawing.Point(278, 33)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.Size = New System.Drawing.Size(127, 97)
        Me.Chart2.TabIndex = 3
        Me.Chart2.Text = "Chart2"
        '
        'Chart3
        '
        ChartArea3.Name = "ChartArea1"
        Me.Chart3.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.Chart3.Legends.Add(Legend3)
        Me.Chart3.Location = New System.Drawing.Point(427, 33)
        Me.Chart3.Name = "Chart3"
        Me.Chart3.Size = New System.Drawing.Size(110, 97)
        Me.Chart3.TabIndex = 4
        Me.Chart3.Text = "Chart3"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(68, 8)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(140, 19)
        Me.TextBox1.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "電場 E ="
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(223, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "*10^8[V/m]"
        '
        'Chart4
        '
        ChartArea4.Name = "ChartArea1"
        Me.Chart4.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Me.Chart4.Legends.Add(Legend4)
        Me.Chart4.Location = New System.Drawing.Point(14, 136)
        Me.Chart4.Name = "Chart4"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart4.Series.Add(Series1)
        Me.Chart4.Size = New System.Drawing.Size(1403, 381)
        Me.Chart4.TabIndex = 8
        Me.Chart4.Text = "Chart4"
        '
        'Chart5
        '
        ChartArea5.Name = "ChartArea1"
        Me.Chart5.ChartAreas.Add(ChartArea5)
        Legend5.Name = "Legend1"
        Me.Chart5.Legends.Add(Legend5)
        Me.Chart5.Location = New System.Drawing.Point(12, 523)
        Me.Chart5.Name = "Chart5"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.Chart5.Series.Add(Series2)
        Me.Chart5.Size = New System.Drawing.Size(1405, 448)
        Me.Chart5.TabIndex = 9
        Me.Chart5.Text = "Chart5"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(821, 4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 19)
        Me.TextBox2.TabIndex = 10
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1455, 959)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Chart5)
        Me.Controls.Add(Me.Chart4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Chart3)
        Me.Controls.Add(Me.Chart2)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents t_i As DataGridViewTextBoxColumn
    Friend WithEvents t_c As DataGridViewTextBoxColumn
    Friend WithEvents Kinetic_Energy As DataGridViewTextBoxColumn
    Friend WithEvents S As DataGridViewTextBoxColumn
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Chart2 As DataVisualization.Charting.Chart
    Friend WithEvents Chart3 As DataVisualization.Charting.Chart
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Chart4 As DataVisualization.Charting.Chart
    Friend WithEvents Chart5 As DataVisualization.Charting.Chart
    Friend WithEvents TextBox2 As TextBox
End Class
