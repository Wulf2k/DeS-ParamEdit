Imports System.IO
Imports System.Reflection
Imports System.Threading

Public Class frmParamEdit

    Shared Version As String
    Shared VersionCheckUrl As String = "http://wulf2k.ca/souls/ParamEdit-ver.txt"

    Dim bytes() As Byte
    Dim paramDef() As paramDefs
    Dim bigEndian As Boolean = True

    Structure paramDefs
        Public paramName As String
        Public paramJName As String
        Public paramJDesc As String
        Public paramType As String
        Public paramSize As UInteger
        Public paramMin As Single
        Public paramMax As Single
    End Structure

    Private Async Sub updatecheck()
        Try
            Dim client As New Net.WebClient()
            Dim content As String = Await client.DownloadStringTaskAsync(VersionCheckUrl)

            Dim lines() As String = content.Split({vbCrLf, vbLf}, StringSplitOptions.None)
            Dim latestVersion = lines(0)
            Dim latestUrl = lines(1)

            If latestVersion > Version.Replace(".", "") Then
                btnUpdate.Tag = latestUrl
                btnUpdate.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub

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


    Private Sub bAdd(ByRef bArr1() As Byte, ByVal bArr2() As Byte)
        Dim startIndex As Integer = bArr1.Length

        Array.Resize(bArr1, bArr1.Length + bArr2.Length)
        bArr2.CopyTo(bArr1, startIndex)
    End Sub

    Private Function StrFromBytes(ByVal loc As UInteger) As String
        Dim Str As String = ""
        Dim cont As Boolean = True

        While cont
            If bytes(loc) > 0 Then
                Str = Str + Convert.ToChar(bytes(loc))
                loc += 1
            Else
                cont = False
            End If
        End While

        Return Str
    End Function
    Private Function bArrFromBytes(ByVal loc As UInteger) As Byte()
        Dim bArray() As Byte = {}

        Dim cont As Boolean = True
        Dim i As Integer = 0

        While cont
            If loc + i < bytes.Length Then
                If bytes(loc + i) > 0 Then
                    ReDim Preserve bArray(bArray.Length)
                    bArray(i) = bytes(loc + i)
                    i += 1
                Else
                    cont = False
                End If
            Else
                cont = False
            End If
        End While

        Return bArray
    End Function

    Private Function Int16ToTwoByte(ByVal val As Integer) As Byte()
        If bigEndian Then
            Return ReverseTwoBytes(BitConverter.GetBytes(Convert.ToInt16(val)))
        Else
            Return BitConverter.GetBytes(Convert.ToInt16(val))
        End If
    End Function
    Private Function Int32ToFourByte(ByVal val As Integer) As Byte()
        If bigEndian Then
            Return ReverseFourBytes(BitConverter.GetBytes(Convert.ToInt32(val)))
        Else
            Return BitConverter.GetBytes(Convert.ToInt32(val))
        End If
    End Function
    Private Function UInt16TotwoByte(ByVal val As UInteger) As Byte()
        If bigEndian Then
            Return ReverseTwoBytes(BitConverter.GetBytes(Convert.ToUInt16(val)))
        Else
            Return BitConverter.GetBytes(Convert.ToUInt16(val))
        End If
    End Function
    Private Function UInt32ToFourByte(ByVal val As UInteger) As Byte()
        If bigEndian Then
            Return ReverseFourBytes(BitConverter.GetBytes(Convert.ToUInt32(val)))
        Else
            Return BitConverter.GetBytes(Convert.ToUInt32(val))
        End If
    End Function
    Private Function FloatToFourByte(ByVal val As String) As Byte()
        If IsNumeric(val) Then
            If bigEndian Then
                Return ReverseFourBytes(BitConverter.GetBytes(Convert.ToSingle(val)))
            Else
                Return BitConverter.GetBytes(Convert.ToSingle(val))
            End If
        Else
            Return {0, 0, 0, 0}
        End If
    End Function
    Private Function ReverseFourBytes(ByVal byt() As Byte)
        Return {byt(3), byt(2), byt(1), byt(0)}
    End Function
    Private Function ReverseTwoBytes(ByVal byt() As Byte)
        Return {byt(1), byt(0)}
    End Function
    Private Sub InsBytes(ByVal loc As UInteger, ByVal byt As Byte())
        For i = 0 To byt.Length - 1
            bytes(loc + i) = byt(i)
        Next
    End Sub

    Private Function SingleFromFour(ByVal loc As UInteger) As Single
        Dim bArray(3) As Byte

        For i = 0 To 3
            bArray(3 - i) = bytes(loc + i)
        Next
        If Not bigEndian Then bArray = ReverseFourBytes(bArray)
        Return BitConverter.ToSingle(bArray, 0)
    End Function
    Private Function SIntFromTwo(ByVal loc As UInteger) As Int16
        Dim tmpint As Integer = 0
        Dim bArray(1) As Byte

        For i = 0 To 1
            bArray(1 - i) = bytes(loc + i)
        Next
        If Not bigEndian Then bArray = ReverseTwoBytes(bArray)
        tmpint = BitConverter.ToInt16(bArray, 0)
        Return tmpint
    End Function
    Private Function SIntFromFour(ByVal loc As UInteger) As Integer
        Dim tmpint As Integer = 0
        Dim bArray(3) As Byte

        For i = 0 To 3
            bArray(3 - i) = bytes(loc + i)
        Next
        If Not bigEndian Then bArray = ReverseFourBytes(bArray)
        tmpint = BitConverter.ToInt32(bArray, 0)
        Return tmpint
    End Function
    Private Function UIntFromTwo(ByVal loc As UInteger) As UInteger
        Dim tmpUint As UInteger = 0

        If bigEndian Then
            For i = 0 To 1
                tmpUint += Convert.ToUInt16(bytes(loc + i)) * &H100 ^ (1 - i)
            Next
        Else
            For i = 0 To 1
                tmpUint += Convert.ToUInt16(bytes(loc + i)) * &H100 ^ (i)
            Next
        End If

        Return tmpUint
    End Function
    Private Function UIntFromFour(ByVal loc As UInteger) As UInteger
        Dim tmpUint As UInteger = 0

        If bigEndian Then
            For i = 0 To 3
                tmpUint += Convert.ToUInt32(bytes(loc + i)) * &H100 ^ (3 - i)
            Next
        Else
            For i = 0 To 3
                tmpUint += Convert.ToUInt32(bytes(loc + i)) * &H100 ^ (i)
            Next
        End If

        Return tmpUint
    End Function


    Private Sub txt_Drop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles txtParamdef.DragDrop, txtParam.DragDrop
        Dim file() As String = e.Data.GetData(DataFormats.FileDrop)
        sender.Text = file(0)
    End Sub
    Private Sub txt_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles txtParamdef.DragEnter, txtParam.DragEnter
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        dgvParams.Rows.Clear()
        dgvParams.Columns.Clear()

        bytes = File.ReadAllBytes(txtParamdef.Text)

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

        length = UIntFromFour(&H0)

        If Not length = bytes.Length Then
            bigEndian = False
            length = UIntFromFour(&H0)
        End If

        startOffset = UIntFromTwo(&H4)
        numEntries = UIntFromTwo(&H8)
        entryLength = UIntFromTwo(&HA)


        ReDim paramDef(numEntries - 1)


        dgvParams.Columns.Add("ID (Hex)", "ID (Hex)")
        dgvParams.Columns.Add("ID (Dec)", "ID (Dec)")

        For i = 0 To numEntries - 1
            paramType = StrFromBytes(startOffset + &H40 + (entryLength * i))
            paramMin = SingleFromFour(startOffset + &H54 + (entryLength * i))
            paramMax = SingleFromFour(startOffset + &H58 + (entryLength * i))
            paramSize = UIntFromFour(startOffset + &H64 + (entryLength * i))
            paramName = StrFromBytes(startOffset + &H8C + (entryLength * i))

            If paramName.Contains(":") Then
                paramType = "bool"
                paramSize = 0
            End If

            If paramType = "dummy8" Then
                paramSize = 0
            End If

            paramDef(i).paramName = paramName
            paramDef(i).paramType = paramType
            paramDef(i).paramSize = paramSize
            paramDef(i).paramMin = paramMin
            paramDef(i).paramMax = paramMax

            

            dgvParams.Columns.Add(paramDef(i).paramName, paramDef(i).paramName)
        Next

        bytes = File.ReadAllBytes(txtParam.Text)
        numEntries = UIntFromTwo(&HA)

        For i = 0 To numEntries - 1
            Dim row(paramDef.Count + 2) As String

            row(0) = Hex(UIntFromFour(&H30 + (&HC * i)))
            row(1) = UIntFromFour(&H30 + (&HC * i))



            offset = UIntFromFour(&H34 + (&HC * i))

            paramDefOffset = 0

            Dim bitarray = 1


            For j = 0 To paramDef.Length - 1
                Select Case paramDef(j).paramType
                    Case "bool"
                        row(j + 2) = ((CByte(bytes(offset + paramDefOffset)) and bitarray) > 0)
                        bitarray = bitarray * 2
                        If bitarray = 256 Then
                            bitarray = 1
                            paramDefOffset += 1
                        End If
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

                        paramDefOffset += pad
                    Case "f32"
                        row(j + 2) = SingleFromFour(offset + paramDefOffset)
                    Case "s8"
                        row(j + 2) = CSByte(bytes(offset + paramDefOffset))
                    Case "s16"
                        row(j + 2) = SIntFromTwo(offset + paramDefOffset)
                    Case "s32"
                        row(j + 2) = SIntFromFour(offset + paramDefOffset)
                    Case "u8"
                        row(j + 2) = CByte(bytes(offset + paramDefOffset))
                    Case "u16"
                        row(j + 2) = UIntFromTwo(offset + paramDefOffset)
                    Case "u32"
                        row(j + 2) = UIntFromFour(offset + paramDefOffset)
                End Select

                paramDefOffset += paramDef(j).paramSize


            Next

            dgvParams.Rows.Add(row)

            offset = UIntFromFour(&H38 + (&HC * i))

        Next
        dgvParams.Columns(1).Frozen = True
        dgvParams.AutoResizeColumns()

        For each column as DataGridViewColumn In dgvparams.columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next


    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        bytes = File.ReadAllBytes(txtParam.Text)
        Dim paramDefOffset As UInteger
        Dim offset As UInteger

        For i = 0 To dgvParams.Rows.Count - 2
            paramDefOffset = 0

            offset = UIntFromFour(&H34 + (&HC * i))

            Dim bitfield = 0
            Dim bitval = 0


            For j = 0 To paramDef.Length - 1
                Select Case paramDef(j).paramType
                    Case "bool"
                        If dgvParams.Rows(i).Cells(j + 2).FormattedValue = "True" Then
                            bitval = bitval + 2 ^ bitfield
                        End If
                        bitfield += 1
                        If bitfield = 8 Then
                            bytes(offset + paramDefOffset) = Convert.ToByte(bitval)
                            paramDefOffset += 1
                            bitval = 0
                            bitfield = 0
                        End If
                    Case "dummy8"
                        Dim pad = paramDef(j).paramName

                        If pad.Contains("[") Then
                            pad = pad.Split("[")(1)
                            pad = pad.Replace("]", "")
                        End If

                        If bitfield > 0 Then
                            bitfield = 0
                            bytes(offset + paramDefOffset) = Convert.ToByte(bitval)
                            paramDefOffset += 1
                        End If

                        For k = 0 To val(pad) - 1
                            bytes(offset + paramDefOffset) = Convert.ToByte(0)
                            paramDefOffset += 1
                        Next
                    Case "f32"
                        InsBytes(offset + paramDefOffset, FloatToFourByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "s8"
                        bytes(offset + paramDefOffset) = Convert.ToSByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "s16"
                        InsBytes(offset + paramDefOffset, Int16ToTwoByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "s32"
                        InsBytes(offset + paramDefOffset, Int32ToFourByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "u8"
                        bytes(offset + paramDefOffset) = Convert.ToByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "u16"
                        InsBytes(offset + paramDefOffset, UInt16TotwoByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "u32"
                        InsBytes(offset + paramDefOffset, UInt32ToFourByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                End Select
                paramDefOffset += paramDef(j).paramSize
            Next
        Next
        File.WriteAllBytes(txtParam.Text, bytes)
        MsgBox("Save complete.")
    End Sub

    Private Sub ParamEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim systemType As Type = dgvParams.GetType()
        Dim propertyInfo As PropertyInfo = systemType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        propertyInfo.SetValue(dgvParams, True, Nothing)


        Version = lblVer.Text

        Dim oldFileArg As String = Nothing
        For Each arg In Environment.GetCommandLineArgs().Skip(1)
            If arg.StartsWith("--old-file=") Then
                oldFileArg = arg.Substring("--old-file=".Length)
            Else
                MsgBox("Unknown command line arguments")
                oldFileArg = Nothing
                Exit For
            End If
        Next
        If oldFileArg IsNot Nothing Then
            If oldFileArg.EndsWith(".old") Then
                Dim t = New Thread(
                    Sub()
                    Try
                        'Give the old version time to shut down
                        Thread.Sleep(1000)
                        File.Delete(oldFileArg)
                    Catch ex As Exception
                        Me.Invoke(Function() MsgBox("Deleting old version failed: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation))
                    End Try
                End Sub)
                t.Start()
            Else
                MsgBox("Deleting old version failed: Invalid filename ", MsgBoxStyle.Exclamation)
            End If
        End If


        updatecheck()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim updateWindow As New UpdateWindow(sender.tag)
        updateWindow.ShowDialog()
        If updateWindow.WasSuccessful Then
            Process.Start(updateWindow.NewAssembly, """--old-file=" & updateWindow.OldAssembly & """")
            Me.Close()
        End If
    End Sub
End Class
