<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParamEdit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtParamdef = New System.Windows.Forms.TextBox()
        Me.lblGAFile = New System.Windows.Forms.Label()
        Me.btnBrowseParamdef = New System.Windows.Forms.Button()
        Me.txtParam = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBrowseParam = New System.Windows.Forms.Button()
        Me.dgvParams = New System.Windows.Forms.DataGridView()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblVer = New System.Windows.Forms.Label()
        Me.txtParamName = New System.Windows.Forms.TextBox()
        Me.lblParamName = New System.Windows.Forms.Label()
        Me.txtUnk0x8 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUnk0x6 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnExportCSV = New System.Windows.Forms.Button()
        Me.btnImportCSV = New System.Windows.Forms.Button()
        CType(Me.dgvParams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtParamdef
        '
        Me.txtParamdef.AllowDrop = True
        Me.txtParamdef.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtParamdef.Location = New System.Drawing.Point(134, 22)
        Me.txtParamdef.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtParamdef.Name = "txtParamdef"
        Me.txtParamdef.Size = New System.Drawing.Size(658, 26)
        Me.txtParamdef.TabIndex = 29
        '
        'lblGAFile
        '
        Me.lblGAFile.AutoSize = True
        Me.lblGAFile.Location = New System.Drawing.Point(46, 26)
        Me.lblGAFile.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGAFile.Name = "lblGAFile"
        Me.lblGAFile.Size = New System.Drawing.Size(78, 20)
        Me.lblGAFile.TabIndex = 31
        Me.lblGAFile.Text = "Paramdef"
        '
        'btnBrowseParamdef
        '
        Me.btnBrowseParamdef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseParamdef.Location = New System.Drawing.Point(798, 18)
        Me.btnBrowseParamdef.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnBrowseParamdef.Name = "btnBrowseParamdef"
        Me.btnBrowseParamdef.Size = New System.Drawing.Size(112, 35)
        Me.btnBrowseParamdef.TabIndex = 30
        Me.btnBrowseParamdef.Text = "Browse"
        Me.btnBrowseParamdef.UseVisualStyleBackColor = True
        '
        'txtParam
        '
        Me.txtParam.AllowDrop = True
        Me.txtParam.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtParam.Location = New System.Drawing.Point(134, 66)
        Me.txtParam.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtParam.Name = "txtParam"
        Me.txtParam.Size = New System.Drawing.Size(658, 26)
        Me.txtParam.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(69, 71)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 20)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Param"
        '
        'btnBrowseParam
        '
        Me.btnBrowseParam.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBrowseParam.Location = New System.Drawing.Point(798, 63)
        Me.btnBrowseParam.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnBrowseParam.Name = "btnBrowseParam"
        Me.btnBrowseParam.Size = New System.Drawing.Size(112, 35)
        Me.btnBrowseParam.TabIndex = 33
        Me.btnBrowseParam.Text = "Browse"
        Me.btnBrowseParam.UseVisualStyleBackColor = True
        '
        'dgvParams
        '
        Me.dgvParams.AllowDrop = True
        Me.dgvParams.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvParams.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParams.Location = New System.Drawing.Point(18, 226)
        Me.dgvParams.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgvParams.Name = "dgvParams"
        Me.dgvParams.RowHeadersWidth = 62
        Me.dgvParams.Size = New System.Drawing.Size(892, 712)
        Me.dgvParams.TabIndex = 36
        '
        'btnOpen
        '
        Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpen.Location = New System.Drawing.Point(676, 108)
        Me.btnOpen.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(117, 35)
        Me.btnOpen.TabIndex = 37
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(798, 108)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 35)
        Me.btnSave.TabIndex = 38
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblVer
        '
        Me.lblVer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVer.AutoSize = True
        Me.lblVer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblVer.Location = New System.Drawing.Point(796, 197)
        Me.lblVer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVer.Name = "lblVer"
        Me.lblVer.Size = New System.Drawing.Size(111, 20)
        Me.lblVer.TabIndex = 77
        Me.lblVer.Text = "2018.03.23.22"
        '
        'txtParamName
        '
        Me.txtParamName.AllowDrop = True
        Me.txtParamName.Location = New System.Drawing.Point(134, 192)
        Me.txtParamName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtParamName.Name = "txtParamName"
        Me.txtParamName.Size = New System.Drawing.Size(308, 26)
        Me.txtParamName.TabIndex = 79
        '
        'lblParamName
        '
        Me.lblParamName.AutoSize = True
        Me.lblParamName.Location = New System.Drawing.Point(18, 197)
        Me.lblParamName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblParamName.Name = "lblParamName"
        Me.lblParamName.Size = New System.Drawing.Size(101, 20)
        Me.lblParamName.TabIndex = 80
        Me.lblParamName.Text = "Param Name"
        '
        'txtUnk0x8
        '
        Me.txtUnk0x8.AllowDrop = True
        Me.txtUnk0x8.Location = New System.Drawing.Point(512, 192)
        Me.txtUnk0x8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUnk0x8.Name = "txtUnk0x8"
        Me.txtUnk0x8.Size = New System.Drawing.Size(94, 26)
        Me.txtUnk0x8.TabIndex = 81
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(458, 197)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 20)
        Me.Label2.TabIndex = 82
        Me.Label2.Text = "(0x8?)"
        '
        'txtUnk0x6
        '
        Me.txtUnk0x6.AllowDrop = True
        Me.txtUnk0x6.Location = New System.Drawing.Point(512, 152)
        Me.txtUnk0x6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUnk0x6.Name = "txtUnk0x6"
        Me.txtUnk0x6.Size = New System.Drawing.Size(94, 26)
        Me.txtUnk0x6.TabIndex = 83
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(458, 157)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 20)
        Me.Label3.TabIndex = 84
        Me.Label3.Text = "(0x6?)"
        '
        'btnExportCSV
        '
        Me.btnExportCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExportCSV.Location = New System.Drawing.Point(798, 152)
        Me.btnExportCSV.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnExportCSV.Name = "btnExportCSV"
        Me.btnExportCSV.Size = New System.Drawing.Size(112, 35)
        Me.btnExportCSV.TabIndex = 86
        Me.btnExportCSV.Text = "Export CSV"
        Me.btnExportCSV.UseVisualStyleBackColor = True
        '
        'btnImportCSV
        '
        Me.btnImportCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImportCSV.Location = New System.Drawing.Point(681, 152)
        Me.btnImportCSV.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnImportCSV.Name = "btnImportCSV"
        Me.btnImportCSV.Size = New System.Drawing.Size(112, 35)
        Me.btnImportCSV.TabIndex = 85
        Me.btnImportCSV.Text = "Import CSV"
        Me.btnImportCSV.UseVisualStyleBackColor = True
        '
        'frmParamEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(926, 951)
        Me.Controls.Add(Me.btnExportCSV)
        Me.Controls.Add(Me.btnImportCSV)
        Me.Controls.Add(Me.txtUnk0x6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtUnk0x8)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtParamName)
        Me.Controls.Add(Me.lblParamName)
        Me.Controls.Add(Me.lblVer)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.dgvParams)
        Me.Controls.Add(Me.txtParam)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBrowseParam)
        Me.Controls.Add(Me.txtParamdef)
        Me.Controls.Add(Me.lblGAFile)
        Me.Controls.Add(Me.btnBrowseParamdef)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmParamEdit"
        Me.Text = "Wulf's Souls Series Parameter Editor"
        CType(Me.dgvParams,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents txtParamdef As System.Windows.Forms.TextBox
    Friend WithEvents lblGAFile As System.Windows.Forms.Label
    Friend WithEvents btnBrowseParamdef As System.Windows.Forms.Button
    Friend WithEvents txtParam As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseParam As System.Windows.Forms.Button
    Friend WithEvents dgvParams As System.Windows.Forms.DataGridView
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblVer As Label
    Friend WithEvents txtParamName As TextBox
    Friend WithEvents lblParamName As Label
    Friend WithEvents txtUnk0x8 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUnk0x6 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnExportCSV As Button
    Friend WithEvents btnImportCSV As Button
End Class
