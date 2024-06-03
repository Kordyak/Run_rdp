Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Module Module1

    Sub Main()

        Console.WriteLine("EnterKey: ")
        Dim key_new = Console.ReadLine


        ' Читаем старый RDP
        Dim Files() As String = IO.Directory.GetFiles(".\", "*.rdp", IO.SearchOption.TopDirectoryOnly)
        Dim wsa_rdp = My.Computer.FileSystem.ReadAllText(Files(0))

        ' Ищем старый ключ
        Dim r = New Regex("gatewayaccesstoken:s:npr\\\w*-(\d*)")
        Dim key_old As String = r.Match(wsa_rdp).Groups(1).Value
        ' Заменяем его на новый
        wsa_rdp = wsa_rdp.Replace(key_old, key_new)


        ' Записываем файл
        Dim fs As FileStream = File.Create(Files(0))
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(wsa_rdp)
        fs.Write(info, 0, info.Length)
        fs.Close()


        Process.Start(fs.Name)

    End Sub

End Module
