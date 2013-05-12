namespace UIAutomationSampleForm

open System
open System.Collections.ObjectModel
open System.ComponentModel
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open System.Windows.Media
open System.Windows.Input
open System.IO

type MainForm() =
    let _wpf = Application.LoadComponent(new Uri("MainForm.xaml", System.UriKind.Relative)) :?> Window
    let _gp = _wpf.FindName("groupBox1") :?> GroupBox
    let _rb1 = _wpf.FindName("radioButton1") :?> RadioButton
    let _rb2 = _wpf.FindName("radioButton2") :?> RadioButton
    let _rb3 = _wpf.FindName("radioButton3") :?> RadioButton
    let _txtView = _wpf.FindName("txtView") :?> TextBox
    let _btnView = _wpf.FindName("btnView") :?> Button
    let _btnTest = _wpf.FindName("btnTest") :?> Button
    let _txtNone = _wpf.FindName("textBox2") :?> TextBox
    let _chk = _wpf.FindName("checkBox1") :?> CheckBox
    do
        _btnView.Click.Add(fun _ -> _txtView.Text <- "ああああ")
        _btnTest.Click.Add(fun _ -> MessageBox.Show("メッセージテスト") |> ignore)
        ()
    member x.WPF = _wpf

module Program =
    [<STAThread>]
    [<EntryPoint>]
    let run(_) =
//        // TextBoxがフォーカスを受け取ると全選択する
//        EventManager.RegisterClassHandler(
//            typeof<TextBox>,
//            TextBox.GotFocusEvent,
//            new RoutedEventHandler(fun a b ->
//                match a with
//                | :? TextBox as t -> t.SelectAll()
//                | _ -> ()))
//        let wpf = Application.LoadComponent(new Uri("ChannelEditor.xaml", System.UriKind.Relative)) :?> Window
//        let model = new ChannelEditorModel(wpf)
//        (new Application()).Run(wpf)
        let form = new MainForm()
        (new Application()).Run(form.WPF)
