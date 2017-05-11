Imports System.IO
Imports System.Reflection
Imports System.Threading

Public Class frmParamEdit



    'TODO:  EquipParamWeapon - Figure out "WEP_BASE_CHANGE_CATEGORY:6"






    Shared Version As String
    Shared VersionCheckUrl As String = "http://wulf2k.ca/souls/ParamEdit-ver.txt"

    Dim paramDef() As paramDefs
    Dim bigEndian As Boolean = True

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

        fs.Position = loc

        While cont
            byt = fs.ReadByte

            If byt > 0 Then
                AscStr = AscStr & Convert.ToChar(byt)
            Else
                cont = False
            End If
        End While

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
        byt = System.Text.Encoding.ASCII.GetBytes(str)
        fs.Write(byt, 0, byt.Length)
    End Sub


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


        fs = New IO.FileStream(txtParamdef.Text, IO.FileMode.Open)

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
                paramType = "bool"
                paramSize = paramName.Split(":")(1)
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


        fs = New IO.FileStream(txtParam.Text, IO.FileMode.Open)

        numEntries = RUInt16(&HA)

        For i = 0 To numEntries - 1
            Dim row(paramDef.Count + 2) As String

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
                        row(j + 2) = RUInt8(offset + paramDefOffset)
                    Case "u16"
                        row(j + 2) = RUInt16(offset + paramDefOffset)
                    Case "u32"
                        row(j + 2) = RUInt32(offset + paramDefOffset)
                End Select

                paramDefOffset += Math.Floor(paramDef(j).paramSize / 8)


            Next

            dgvParams.Rows.Add(row)

            offset = RUInt32(&H38 + (&HC * i))

        Next
        fs.Close()

        dgvParams.Columns(1).Frozen = True
        dgvParams.AutoResizeColumns()

        For Each column As DataGridViewColumn In dgvParams.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next


    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        fs = New IO.FileStream(txtParam.Text, IO.FileMode.Open)
        Dim paramDefOffset As UInteger
        Dim offset As UInteger

        For i = 0 To dgvParams.Rows.Count - 2
            paramDefOffset = 0

            offset = RUInt32(&H34 + (&HC * i))

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

                        For k = 0 To Val(pad) - 1
                            'WUInt8(offset + paramDefOffset + k, 0)
                        Next
                    Case "f32"
                        WSingle(offset + paramDefOffset, dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "s8"
                        WInt8(offset + paramDefOffset, dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "s16"
                        WInt16(offset + paramDefOffset, dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "s32"
                        WInt32(offset + paramDefOffset, dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "u8"
                        WUInt8(offset + paramDefOffset, dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "u16"
                        WUInt16(offset + paramDefOffset, dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                    Case "u32"
                        WUInt32(offset + paramDefOffset, dgvParams.Rows(i).Cells(j + 2).FormattedValue)
                End Select
                paramDefOffset += Math.Floor(paramDef(j).paramSize / 8)
            Next
        Next
        fs.Close()
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
