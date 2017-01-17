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
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.lblVer = New System.Windows.Forms.Label()
        CType(Me.dgvParams,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'txtParamdef
        '
        Me.txtParamdef.AllowDrop = true
        Me.txtParamdef.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtParamdef.Location = New System.Drawing.Point(89, 14)
        Me.txtParamdef.Name = "txtParamdef"
        Me.txtParamdef.Size = New System.Drawing.Size(440, 20)
        Me.txtParamdef.TabIndex = 29
        '
        'lblGAFile
        '
        Me.lblGAFile.AutoSize = true
        Me.lblGAFile.Location = New System.Drawing.Point(31, 17)
        Me.lblGAFile.Name = "lblGAFile"
        Me.lblGAFile.Size = New System.Drawing.Size(52, 13)
        Me.lblGAFile.TabIndex = 31
        Me.lblGAFile.Text = "Paramdef"
        '
        'btnBrowseParamdef
        '
        Me.btnBrowseParamdef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnBrowseParamdef.Location = New System.Drawing.Point(532, 12)
        Me.btnBrowseParamdef.Name = "btnBrowseParamdef"
        Me.btnBrowseParamdef.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseParamdef.TabIndex = 30
        Me.btnBrowseParamdef.Text = "Browse"
        Me.btnBrowseParamdef.UseVisualStyleBackColor = true
        '
        'txtParam
        '
        Me.txtParam.AllowDrop = true
        Me.txtParam.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtParam.Location = New System.Drawing.Point(89, 43)
        Me.txtParam.Name = "txtParam"
        Me.txtParam.Size = New System.Drawing.Size(440, 20)
        Me.txtParam.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(46, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Param"
        '
        'btnBrowseParam
        '
        Me.btnBrowseParam.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnBrowseParam.Location = New System.Drawing.Point(532, 41)
        Me.btnBrowseParam.Name = "btnBrowseParam"
        Me.btnBrowseParam.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowseParam.TabIndex = 33
        Me.btnBrowseParam.Text = "Browse"
        Me.btnBrowseParam.UseVisualStyleBackColor = true
        '
        'dgvParams
        '
        Me.dgvParams.AllowUserToAddRows = false
        Me.dgvParams.AllowUserToDeleteRows = false
        Me.dgvParams.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParams.Location = New System.Drawing.Point(12, 100)
        Me.dgvParams.Name = "dgvParams"
        Me.dgvParams.Size = New System.Drawing.Size(595, 510)
        Me.dgvParams.TabIndex = 36
        '
        'btnOpen
        '
        Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnOpen.Location = New System.Drawing.Point(451, 70)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnOpen.TabIndex = 37
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = true
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(532, 70)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 38
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = true
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(12, 69)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(182, 23)
        Me.btnUpdate.TabIndex = 78
        Me.btnUpdate.Text = "Update Param Editor"
        Me.btnUpdate.UseVisualStyleBackColor = true
        Me.btnUpdate.Visible = false
        '
        'lblVer
        '
        Me.lblVer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblVer.AutoSize = true
        Me.lblVer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblVer.Location = New System.Drawing.Point(369, 75)
        Me.lblVer.Name = "lblVer"
        Me.lblVer.Size = New System.Drawing.Size(76, 13)
        Me.lblVer.TabIndex = 77
        Me.lblVer.Text = "2017.01.16.22"
        '
        'frmParamEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(617, 618)
        Me.Controls.Add(Me.btnUpdate)
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
    Friend WithEvents btnUpdate As Button
    Friend WithEvents lblVer As Label
End Class
