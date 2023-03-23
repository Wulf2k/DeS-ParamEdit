﻿Imports System.IO
Imports System.Reflection
Imports System.Threading

Public Class frmParamEdit

    'TODO:  Properly add rows to allow numeric sorting instead of string sorting.



    Shared Version As String

    Dim paramDef() As paramDefs
    Dim bigEndian As Boolean = True


    'To allow drag and drop re-ordering of items
    Private fromIndex As Integer
    Private dragIndex As Integer
    Private dragRect As Rectangle



    Dim fs As FileStream


    Structure paramDefs
        Public paramName As String
        Public paramJName As String
        Public paramJDesc As String
        Public paramType As String
        Public paramSize As UInteger
        Public paramMin As Single
        Public paramMax As Single
    End Structure





    'Drag 'n Drop re-ordering support
    Private Sub DdgvParams_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgvParams.DragDrop
        Dim p As Point = dgvParams.PointToClient(New Point(e.X, e.Y))
        dragIndex = dgvParams.HitTest(p.X, p.Y).RowIndex
        If (e.Effect = DragDropEffects.Move) Then
            Dim dragRow As DataGridViewRow = e.Data.GetData(GetType(DataGridViewRow))
            dgvParams.Rows.RemoveAt(fromIndex)
            dgvParams.Rows.Insert(dragIndex, dragRow)
        End If
    End Sub
    Private Sub dgvParams_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles dgvParams.DragOver
        e.Effect = DragDropEffects.Move
    End Sub
    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvParams.MouseDown
        fromIndex = dgvParams.HitTest(e.X, e.Y).RowIndex
        If fromIndex > -1 Then
            Dim dragSize As Size = SystemInformation.DragSize
            dragRect = New Rectangle(New Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize)
        Else
            dragRect = Rectangle.Empty
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvParams.MouseMove
        If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
            If (dragRect <> Rectangle.Empty AndAlso Not dragRect.Contains(e.X, e.Y)) Then
                dgvParams.DoDragDrop(dgvParams.Rows(fromIndex), DragDropEffects.Move)
            End If
        End If
    End Sub





    'File Browsing
    Private Sub btnBrowseParamdef_Click(sender As Object, e As EventArgs) Handles btnBrowseParamdef.Click
        Dim openDlg As New OpenFileDialog()

        openDlg.Filter = "Paramdef File|*paramdef"
        openDlg.Title = "Open your Paramdef file"

        If openDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtParamdef.Text = openDlg.FileName
        End If
    End Sub
    Private Sub btnBrowseParam_Click(sender As Object, e As EventArgs) Handles btnBrowseParam.Click
        Dim openDlg As New OpenFileDialog()

        openDlg.Filter = "Param File|*param"
        openDlg.Title = "Open your Param file"

        If openDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtParam.Text = openDlg.FileName
        End If
    End Sub



    'File Access Functions
    Private Function RSingle(ByVal loc As UInteger) As Single
        Dim tmpSingle As Single = 0
        Dim byt = New Byte() {0, 0, 0, 0}

        fs.Position = loc
        fs.Read(byt, 0, 4)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        tmpSingle = BitConverter.ToSingle(byt, 0)

        Return tmpSingle
    End Function
    Private Function RInt8(ByVal loc As UInteger) As SByte
        Dim tmpInt8 As Byte
        Dim byt = New Byte() {0}

        fs.Position = loc
        fs.Read(byt, 0, 1)

        tmpInt8 = byt(0)

        Return tmpInt8
    End Function
    Private Function RInt16(ByVal loc As UInteger) As Int16
        Dim tmpInt16 As UInteger = 0
        Dim byt = New Byte() {0, 0}

        fs.Position = loc
        fs.Read(byt, 0, 2)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        tmpInt16 = BitConverter.ToInt16(byt, 0)

        Return tmpInt16
    End Function
    Private Function RInt32(ByVal loc As UInteger) As Int32
        Dim tmpInt32 As UInteger = 0
        Dim byt = New Byte() {0, 0, 0, 0}

        fs.Position = loc
        fs.Read(byt, 0, 4)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        tmpInt32 = BitConverter.ToInt32(byt, 0)

        Return tmpInt32
    End Function
    Private Function RUInt8(ByVal loc As UInteger) As Byte
        Dim tmpUInt8 As Byte
        Dim byt = New Byte() {0}

        fs.Position = loc
        fs.Read(byt, 0, 1)

        tmpUInt8 = byt(0)

        Return tmpUInt8
    End Function
    Private Function RUInt16(ByVal loc As UInteger) As UInt16
        Dim tmpUInt16 As UInteger = 0
        Dim byt = New Byte() {0, 0}

        fs.Position = loc
        fs.Read(byt, 0, 2)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        tmpUInt16 = BitConverter.ToUInt16(byt, 0)

        Return tmpUInt16
    End Function
    Private Function RUInt32(ByVal loc As UInteger) As UInt32
        Dim tmpUInt32 As UInteger = 0
        Dim byt = New Byte() {0, 0, 0, 0}

        fs.Position = loc
        fs.Read(byt, 0, 4)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        tmpUInt32 = BitConverter.ToUInt32(byt, 0)

        Return tmpUInt32
    End Function
    Private Function RAscStr(ByVal loc As UInteger) As String
        Dim AscStr As String = ""
        Dim cont As Boolean = True
        Dim len As Integer = 0
        Dim byt As Byte

        Dim byts() As Byte = {}


        fs.Position = loc

        While cont
            byt = fs.ReadByte

            If byt > 0 Then
                ReDim Preserve byts(byts.Length)
                Array.Copy({byt}, 0, byts, byts.Length - 1, 1)

            Else
                cont = False
            End If
        End While

        AscStr = System.Text.Encoding.GetEncoding("shift-jis").GetString(byts)

        Return AscStr
    End Function


    Private Sub WSingle(ByVal loc As UInteger, ByVal val As Single)
        fs.Position = loc

        Dim byt = BitConverter.GetBytes(val)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        fs.Write(byt, 0, 4)
    End Sub
    Private Sub WInt8(ByVal loc As UInteger, ByVal val As SByte)
        fs.Position = loc

        fs.WriteByte(val)
    End Sub
    Private Sub WInt16(ByVal loc As UInteger, ByVal val As Int16)
        fs.Position = loc

        Dim byt() As Byte
        byt = BitConverter.GetBytes(val)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        fs.Write(byt, 0, 2)
    End Sub
    Private Sub WInt32(ByVal loc As UInteger, ByVal val As Int32)
        fs.Position = loc

        Dim byt() As Byte
        byt = BitConverter.GetBytes(val)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        fs.Write(byt, 0, 4)
    End Sub
    Private Sub WUInt8(ByVal loc As UInteger, ByVal val As Byte)
        fs.Position = loc

        fs.WriteByte(val)
    End Sub
    Private Sub WUInt16(ByVal loc As UInteger, ByVal val As UInt16)
        fs.Position = loc

        Dim byt() As Byte
        byt = BitConverter.GetBytes(val)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        fs.Write(byt, 0, 2)
    End Sub
    Private Sub WUInt32(ByVal loc As UInteger, ByVal val As UInt32)
        fs.Position = loc

        Dim byt() As Byte
        byt = BitConverter.GetBytes(val)

        If bigEndian Then
            Array.Reverse(byt)
        End If

        fs.Write(byt, 0, 4)
    End Sub
    Private Sub WAscStr(ByVal loc As UInteger, ByVal str As String)
        fs.Position = loc

        Dim byt() As Byte
        byt = System.Text.Encoding.GetEncoding("shift-jis").GetBytes(str)
        fs.Write(byt, 0, byt.Length)
    End Sub



    'Drag 'n Drop filenames to open
    Private Sub txt_Drop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles txtParamdef.DragDrop, txtParam.DragDrop
        Dim file() As String = e.Data.GetData(DataFormats.FileDrop)
        sender.Text = file(0)
    End Sub
    Private Sub txt_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles txtParamdef.DragEnter, txtParam.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub




    'Open File
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        dgvParams.Rows.Clear()
        dgvParams.Columns.Clear()


        'I don't support your empty textboxes, but I will fight to the death for your right to have them.
        If txtParamdef.Text = "" Then
            MsgBox("No paramdef entered.")
            Return
        End If
        If txtParam.Text = "" Then
            MsgBox("No param entered.")
            Return
        End If

        If Not File.Exists(txtParamdef.Text) Then
            MsgBox("Paramdef not found.")
            Return
        End If
        If Not File.Exists(txtParam.Text) Then
            MsgBox("Param not found.")
            Return
        End If


        fs = New IO.FileStream(txtParamdef.Text, IO.FileMode.Open)
        'Start reading .paramdef file

        Dim length As UInteger
        Dim startOffset As UInteger
        Dim numEntries As UInteger
        Dim entryLength As UInteger

        Dim paramType As String
        Dim paramSize As UInteger
        Dim paramMin As Single
        Dim paramMax As Single
        Dim paramName As String

        Dim offset As UInteger
        Dim paramDefOffset As UInteger

        length = RUInt32(&H0)

        If Not length = fs.Length Then
            bigEndian = False
            length = RUInt32(&H0)
        End If

        startOffset = RUInt16(&H4)
        numEntries = RUInt16(&H8)
        entryLength = RUInt16(&HA)


        ReDim paramDef(numEntries - 1)


        dgvParams.Columns.Add("ID (Hex)", "ID (Hex)")
        dgvParams.Columns.Add("ID (Dec)", "ID (Dec)")

        For i = 0 To numEntries - 1
            paramType = RAscStr(startOffset + &H40 + (entryLength * i))
            paramMin = RSingle(startOffset + &H54 + (entryLength * i))
            paramMax = RSingle(startOffset + &H58 + (entryLength * i))
            paramSize = RUInt32(startOffset + &H64 + (entryLength * i)) * 8
            paramName = RAscStr(startOffset + &H8C + (entryLength * i))



            If paramName.Contains(":") Then

                'Ugly goddamn hack for DaS - SpEffectVfx.paramdef
                'Fromsoft left a 0x06, 0x09 in a numeric field.
                If paramName.Contains(ChrW(6)) Then
                    paramName = paramName.Split(ChrW(6))(0)
                End If


                paramSize = paramName.Split(":")(1)
                If paramSize = 1 Then
                    paramType = "bool"
                Else
                    paramType = "u8"
                End If
            End If

            If paramType = "dummy8" Then
                If paramName.Contains("[") Then
                    paramSize = paramName.Split("[")(1).Replace("]", "") * 8
                Else
                    paramSize = 8
                End If

            End If
            paramDef(i).paramName = paramName
            paramDef(i).paramType = paramType
            paramDef(i).paramSize = paramSize
            paramDef(i).paramMin = paramMin
            paramDef(i).paramMax = paramMax



            dgvParams.Columns.Add(paramDef(i).paramName, paramDef(i).paramName)
        Next
        fs.Close()
        'End reading .paramdef file


        dgvParams.Columns.Add("description", "Description")


        'Start reading .param file
        fs = New IO.FileStream(txtParam.Text, IO.FileMode.Open)

        txtUnk0x6.Text = RUInt16(&H6)
        txtUnk0x8.Text = RUInt16(&H8)
        numEntries = RUInt16(&HA)
        txtParamName.Text = RAscStr(&HC)

        For i = 0 To numEntries - 1
            Dim row(paramDef.Count + 3) As String

            row(0) = Hex(RUInt32(&H30 + (&HC * i)))
            row(1) = RUInt32(&H30 + (&HC * i))



            offset = RUInt32(&H34 + (&HC * i))

            paramDefOffset = 0

            Dim bitarray = 1


            For j = 0 To paramDef.Length - 1
                Select Case paramDef(j).paramType
                    Case "bool"
                        row(j + 2) = ((RUInt8(offset + paramDefOffset) And bitarray) > 0)
                        For p As UInteger = 0 To paramDef(j).paramSize - 1
                            bitarray = bitarray * 2
                            If bitarray = 256 Then
                                bitarray = 1
                                paramDefOffset += 1
                            End If
                        Next

                    Case "dummy8"
                        row(j + 2) = Nothing
                        Dim pad = paramDef(j).paramName

                        If pad.Contains("[") Then
                            pad = pad.Split("[")(1)
                            pad = pad.Replace("]", "")
                        End If


                        If bitarray > 1 Then
                            bitarray = 1
                            paramDefOffset += 1
                        End If

                        'paramDefOffset += pad
                    Case "f32"
                        row(j + 2) = RSingle(offset + paramDefOffset)
                    Case "s8"
                        row(j + 2) = RInt8(offset + paramDefOffset)
                    Case "s16"
                        row(j + 2) = RInt16(offset + paramDefOffset)
                    Case "s32"
                        row(j + 2) = RInt32(offset + paramDefOffset)
                    Case "u8"

                        'row(j + 2) = RUInt8(offset + paramDefOffset)

                        If paramDef(j).paramSize = 8 Then
                            row(j + 2) = RUInt8(offset + paramDefOffset)
                        Else
                            Dim tmpu8 = 0


                            For p As UInteger = 0 To paramDef(j).paramSize - 1

                                If (RUInt8(offset + paramDefOffset) And bitarray) > 0 Then
                                    tmpu8 = tmpu8 + 2 ^ p
                                End If


                                bitarray = bitarray * 2
                                If bitarray = 256 Then
                                    bitarray = 1
                                    paramDefOffset += 1
                                End If
                            Next

                            row(j + 2) = tmpu8




                        End If
                    Case "u16"
                        row(j + 2) = RUInt16(offset + paramDefOffset)
                    Case "u32"
                        row(j + 2) = RUInt32(offset + paramDefOffset)
                End Select

                paramDefOffset += Math.Floor(paramDef(j).paramSize / 8)


            Next

            row(row.Length - 2) = RAscStr(RUInt32(&H38 + (&HC * i)))

            dgvParams.Rows.Add(row)

            offset = RUInt32(&H38 + (&HC * i))

        Next
        fs.Close()

        dgvParams.Columns(1).Frozen = True
        dgvParams.AutoResizeColumns()

        For Each column As DataGridViewColumn In dgvParams.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        Text = $"{New FileInfo(txtParam.Text).Name} - Wulf's Souls Series Parameter Editor"

    End Sub



    'Save Param File
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        fs = New IO.FileStream(txtParam.Text, IO.FileMode.Create)

        Dim paramTotalSize As Integer = 0
        Dim paramDefOffset As UInteger
        Dim paramDescOffset As Integer
        Dim offset As UInteger

        Dim desc As String


        'Define paramTotalSize through dark, eldritch magic
        Dim bitarray = 1
        For j = 0 To paramDef.Length - 1
            Select Case paramDef(j).paramType
                Case "bool"
                    For p As UInteger = 0 To paramDef(j).paramSize - 1
                        bitarray = bitarray * 2
                        If bitarray = 256 Then
                            bitarray = 1
                            paramTotalSize += 1
                        End If
                    Next
                Case "dummy8"
                    If bitarray > 1 Then
                        bitarray = 1
                        paramTotalSize += 1
                    End If
                Case "u8"
                    If Not paramDef(j).paramSize = 8 Then
                        For p As UInteger = 0 To paramDef(j).paramSize - 1
                            bitarray = bitarray * 2
                            If bitarray = 256 Then
                                bitarray = 1
                                paramTotalSize += 1
                            End If
                        Next
                    End If
            End Select

            paramTotalSize += Math.Floor(paramDef(j).paramSize / 8)
        Next




        Dim paramDataStart As Integer = &H30 + (dgvParams.Rows.Count - 1) * &HC
        Dim ParamDescStart As Integer = paramDataStart + ((dgvParams.Rows.Count - 1) * paramTotalSize)

        WInt32(0, ParamDescStart)
        WInt32(4, paramDataStart)


        'Unknown flags, should be split to bytes instad of Int16s
        WInt16(&H6, txtUnk0x6.Text)
        WInt16(&H8, txtUnk0x8.Text)


        WInt16(&HA, dgvParams.Rows.Count - 1)
        WAscStr(&HC, txtParamName.Text)

        'Unknown hardcoded value
        WInt32(&H2C, &H200)


        paramDescOffset = ParamDescStart
        For i = 0 To dgvParams.Rows.Count - 2
            paramDefOffset = 0
            offset = paramDataStart + (paramTotalSize * i)

            WInt32(&H30 + (i * &HC), Val(dgvParams.Rows(i).Cells("ID (Dec)").Value))
            WInt32(&H34 + (i * &HC), offset)
            WInt32(&H38 + (i * &HC), paramDescOffset)

            desc = dgvParams.Rows(i).Cells("Description").Value & Chr(0)
            WAscStr(paramDescOffset, desc)
            paramDescOffset += System.Text.Encoding.GetEncoding("shift-jis").GetBytes(desc).Length



            Dim bitfield = 0
            Dim bitval = 0

            For j = 0 To paramDef.Length - 1
                Select Case paramDef(j).paramType
                    Case "bool"
                        If dgvParams.Rows(i).Cells(j + 2).FormattedValue = "True" Then
                            bitval = bitval + 2 ^ bitfield
                        End If
                        For p As UInteger = 0 To paramDef(j).paramSize - 1
                            bitfield += 1
                            If bitfield = 8 Then
                                WUInt8(offset + paramDefOffset, Convert.ToByte(bitval))
                                paramDefOffset += 1
                                bitval = 0
                                bitfield = 0
                            End If
                        Next
                    Case "dummy8"
                        Dim pad = paramDef(j).paramName

                        If pad.Contains("[") Then
                            pad = pad.Split("[")(1)
                            pad = pad.Replace("]", "")
                        End If

                        If bitfield > 0 Then
                            bitfield = 0
                            WUInt8(offset + paramDefOffset, Convert.ToByte(bitval))
                            bitval = 0
                            paramDefOffset += 1
                        End If

                    Case "f32"
                        WSingle(offset + paramDefOffset, Val(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "s8"
                        WInt8(offset + paramDefOffset, Val(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "s16"
                        WInt16(offset + paramDefOffset, Val(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "s32"
                        WInt32(offset + paramDefOffset, Val(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "u8"
                        If paramDef(j).paramSize = 8 Then
                            WUInt8(offset + paramDefOffset, Val(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                        Else
                            For p As UInteger = 0 To paramDef(j).paramSize - 1
                                If (val(dgvParams.Rows(i).Cells(j + 2).FormattedValue) And 2 ^ p) > 0 Then
                                    bitval = bitval + 2 ^ bitfield
                                End If

                                bitfield += 1
                                If bitfield = 8 Then
                                    WUInt8(offset + paramDefOffset, Convert.ToByte(bitval))
                                    paramDefOffset += 1
                                    bitval = 0
                                    bitfield = 0
                                End If
                            Next
                        End If

                    Case "u16"
                        WUInt16(offset + paramDefOffset, Val(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "u32"
                        WUInt32(offset + paramDefOffset, Val(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                End Select
                paramDefOffset += Math.Floor(paramDef(j).paramSize / 8)
            Next
        Next
        fs.Close()
        MsgBox("Save complete.")

        Text = $"{New FileInfo(txtParam.Text).Name} - Wulf's Souls Series Parameter Editor"

    End Sub




    'Form Load Operations
    Private Sub ParamEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim systemType As Type = dgvParams.GetType()
        Dim propertyInfo As PropertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvParams, True, Nothing)


        Version = lblVer.Text


    End Sub





    'CSV Handling (With | instead of , )
    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        Dim entries As New List(Of String)
        Dim str As String

        For Each row As DataGridViewRow In dgvParams.Rows
            str = ""
            If Not row.Cells(0).FormattedValue = "" Then
                For Each cell As DataGridViewCell In row.Cells
                    str = str & cell.FormattedValue & "|"
                Next
                entries.Add(str)
            End If
        Next

        File.WriteAllLines(txtParam.Text & ".csv", entries)
        MsgBox("Successfully exported to " & txtParam.Text & ".csv")
    End Sub
    Private Sub btnImportCSV_Click(sender As Object, e As EventArgs) Handles btnImportCSV.Click
        Dim row As New List(Of String)

        If File.Exists(txtParam.Text & ".csv") Then
            dgvParams.Rows.Clear()

            Dim entries = File.ReadAllLines(txtParam.Text & ".csv")
            For Each entry In entries

                For Each cell In entry.Split("|")
                    row.Add(cell)
                Next
                dgvParams.Rows.Add(row.ToArray)
                row.Clear()
            Next

        Else
            MsgBox(txtParam.Text & ".csv not found.")
        End If
    End Sub

    Private Sub onDgvKeyDown(sender As Object, e As KeyEventArgs) Handles dgvParams.KeyDown
        If (e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.V) = False Then
            Return
        End If

        Dim o = CType(Clipboard.GetDataObject(), DataObject)
        If o.GetDataPresent(DataFormats.Text) = False Then
            Return
        End If
        Dim text = o.GetData(DataFormats.Text).ToString
        text = text.Replace(vbCr, "").TrimEnd(vbLf)
        Dim lines As String() = text.Split(vbLf)

        Dim sourceRows = New List(Of String())(lines.Length)
        Dim sourceMaxColumnCount = 0
        Dim sourceRowCount = lines.Length
        For i = 0 To lines.Length - 1
            Dim words = lines(i).Split(vbTab)
            sourceRows.Add(words)

            If words.Count > sourceMaxColumnCount Then
                sourceMaxColumnCount = words.Count
            End If
        Next

        Dim dgv = CType(sender, DataGridView)

        Dim cell As DataGridViewCell = dgv.SelectedCells(dgv.SelectedCells.Count - 1)
        Dim startColumn = cell.ColumnIndex
        Dim endColumn = startColumn + sourceMaxColumnCount - 1
        Dim startRow = cell.RowIndex
        Dim endRow = startRow + sourceRowCount - 1

        If endRow > dgv.RowCount - 1 Then
            endRow = dgv.RowCount - 1
        End If
        If endColumn > dgv.ColumnCount - 1 Then
            endColumn = dgv.ColumnCount - 1
        End If

        Dim destColumnCount = endColumn - startColumn + 1
        Dim destRowCount = endRow - startRow + 1

        For x = 0 To destColumnCount - 1
            For y = 0 To destRowCount - 1
                Dim newValue As String = ""
                If x < sourceRows(y).Count Then
                    newValue = sourceRows(y)(x)
                End If
                dgv.Rows(startRow + y).Cells(startColumn + x).Value = newValue
            Next
        Next
    End Sub
End Class
