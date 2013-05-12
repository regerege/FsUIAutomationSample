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

    let radiobutton_event (a:RoutedEventArgs) =
        let rb = a.Source :?> RadioButton
        _txtView.Text <- Convert.ToString(rb.Content)
        ()
    do
        _btnView.Click.Add(fun _ -> _txtView.Text <- "ああああ")
        _btnTest.Click.Add(fun _ -> MessageBox.Show("メッセージテスト") |> ignore)
        _rb1.Click.Add radiobutton_event
        _rb2.Click.Add radiobutton_event
        _rb3.Click.Add radiobutton_event
        ()
    member x.WPF = _wpf

module Program =
    [<STAThread>]
    [<EntryPoint>]
    let run(_) =
        let form = new MainForm()
        (new Application()).Run(form.WPF)
