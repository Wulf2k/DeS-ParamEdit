Imports System.IO

Public Class DeSParam

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

    Private Sub btnBrowseParamdef_Click(sender As Object, e As EventArgs) Handles btnBrowseParamdef.Click


        Dim openDlg As New OpenFileDialog()

        openDlg.Filter = "DeS Paramdef File|*paramdef"
        openDlg.Title = "Open your Paramdef file"

        If openDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtParamdef.Text = openDlg.FileName
        End If


    End Sub
    Private Sub btnBrowseParam_Click(sender As Object, e As EventArgs) Handles btnBrowseParam.Click
        Dim openDlg As New OpenFileDialog()

        openDlg.Filter = "DeS Param File|*param"
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


    Private Sub btnExtract_Click(sender As Object, e As EventArgs)
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
        Dim paramDescOffset As UInteger


        Dim offset As UInteger
        Dim paramDefOffset As UInteger
        Dim output As Byte() = {}


        length = UIntFromFour(&H0)
        startOffset = UIntFromTwo(&H4)
        numEntries = UIntFromTwo(&H8)
        entryLength = UIntFromTwo(&HA)

        paramDescOffset = numEntries * entryLength + &H30

        ReDim paramDef(numEntries - 1)

        bAdd(output, System.Text.Encoding.ASCII.GetBytes("Name,VarType,VarSize,Min,Max,JPName,JPDesc" & Environment.NewLine))
        For i = 0 To numEntries - 1
            paramType = StrFromBytes(startOffset + &H40 + (entryLength * i))
            paramMin = SingleFromFour(startOffset + &H54 + (entryLength * i))
            paramMax = SingleFromFour(startOffset + &H58 + (entryLength * i))
            paramSize = UIntFromFour(startOffset + &H64 + (entryLength * i))
            paramName = StrFromBytes(startOffset + &H8C + (entryLength * i))



            paramDef(i).paramName = paramName
            paramDef(i).paramType = paramType
            paramDef(i).paramSize = paramSize
            paramDef(i).paramMin = paramMin
            paramDef(i).paramMax = paramMax

            bAdd(output, System.Text.Encoding.ASCII.GetBytes(paramName & "," & paramType & ",0x" & Hex(paramSize) & "," & _
                Math.Round(paramMin, 2) & "," & Math.Round(paramMax, 2) & ","))
            bAdd(output, bArrFromBytes(startOffset + (entryLength * i)))
            bAdd(output, System.Text.Encoding.ASCII.GetBytes(","))
            If Not paramDef(i).paramType = "dummy8" Then
                bAdd(output, bArrFromBytes(paramDescOffset))
                paramDescOffset += bArrFromBytes(paramDescOffset).Length + 1
            End If
            bAdd(output, System.Text.Encoding.ASCII.GetBytes(Environment.NewLine))
        Next
        File.WriteAllBytes(txtParamdef.Text & ".csv", output)

        output = System.Text.Encoding.ASCII.GetBytes("ID,")
        For i = 0 To paramDef.Length - 1
            bAdd(output, System.Text.Encoding.ASCII.GetBytes(paramDef(i).paramName & ","))
        Next
        bAdd(output, System.Text.Encoding.ASCII.GetBytes(Environment.NewLine))
        bytes = File.ReadAllBytes(txtParam.Text)
        numEntries = UIntFromTwo(&HA)

        For i = 0 To numEntries - 1
            bAdd(output, System.Text.Encoding.ASCII.GetBytes(Hex(UIntFromFour(&H30 + (&HC * i))) & ","))



            offset = UIntFromFour(&H34 + (&HC * i))
            paramDefOffset = 0

            For j = 0 To paramDef.Length - 1
                Select Case paramDef(j).paramType
                    Case "dummy8"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(","))
                    Case "f32"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(SingleFromFour(offset + paramDefOffset) & ","))
                    Case "s8"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(Convert.ToInt16(bytes(offset + paramDefOffset)) & ","))
                    Case "s16"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(SIntFromTwo(offset + paramDefOffset) & ","))
                    Case "s32"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(SIntFromFour(offset + paramDefOffset) & ","))
                    Case "u8"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(Convert.ToInt16(bytes(offset + paramDefOffset)) & ","))
                    Case "u16"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(UIntFromTwo(offset + paramDefOffset) & ","))
                    Case "u32"
                        bAdd(output, System.Text.Encoding.ASCII.GetBytes(UIntFromFour(offset + paramDefOffset) & ","))
                End Select
                paramDefOffset += paramDef(j).paramSize
            Next

            offset = UIntFromFour(&H38 + (&HC * i))


            REM txtOutput.Text += StrFromBytes(offset) & Environment.NewLine
            bAdd(output, bArrFromBytes(offset))
            bAdd(output, System.Text.Encoding.ASCII.GetBytes(Environment.NewLine))
        Next
        File.WriteAllBytes(txtParam.Text & ".csv", output)

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

            For j = 0 To paramDef.Length - 1
                Select Case paramDef(j).paramType
                    Case "dummy8"
                        row(j + 2) = Nothing
                    Case "f32"
                        row(j + 2) = SingleFromFour(offset + paramDefOffset)
                    Case "s8"
                        row(j + 2) = Convert.ToInt16(bytes(offset + paramDefOffset))
                    Case "s16"
                        row(j + 2) = SIntFromTwo(offset + paramDefOffset)
                    Case "s32"
                        row(j + 2) = SIntFromFour(offset + paramDefOffset)
                    Case "u8"
                        row(j + 2) = Convert.ToInt16(bytes(offset + paramDefOffset))
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

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        bytes = File.ReadAllBytes(txtParam.Text)
        Dim paramDefOffset As UInteger
        Dim offset As UInteger

        For i = 0 To dgvParams.Rows.Count - 2
            paramDefOffset = 0

            offset = UIntFromFour(&H34 + (&HC * i))

            For j = 0 To paramDef.Length - 1
                Select Case paramDef(j).paramType
                    Case "dummy8"
                        bytes(offset + paramDefOffset) = Convert.ToByte(0)
                    Case "f32"
                        InsBytes(offset + paramDefOffset, FloatToFourByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
                    Case "s8"
                        InsBytes(offset + paramDefOffset, Int16ToTwoByte(dgvParams.Rows(i).Cells(j + 2).FormattedValue))
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
End Class
