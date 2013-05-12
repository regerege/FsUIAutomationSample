namespace UIAutomationSample.UIControls
open System
open System.Windows.Automation
open System.Diagnostics
open System.Drawing
open Microsoft.Test.Input

/// UIAutomationの共通補助関数
module UIControlCommon =
    let scope = TreeScope.Element ||| TreeScope.Descendants
    let getElement condition (form:AutomationElement) = form.FindFirst(scope, condition)
    let getProperty (name:string) = new PropertyCondition(AutomationElement.AutomationIdProperty, name)
    let getPattern<'T when 'T :> BasePattern> (element:AutomationElement) =
        let t = typedefof<'T>
        let p = t.GetField("Pattern").GetValue(null) :?> AutomationPattern
        unbox<'T>(element.GetCurrentPattern(p))

/// WPFコントロールのように扱えるようにするためのクラス
type UIControlBase (root, name) =
    let root = UIControlCommon.getElement (UIControlCommon.getProperty name) root
    let _current = root.Current
    do
        Trace.WriteLine <| sprintf "Name: %s" _current.AutomationId
        Trace.Indent()
        root.GetSupportedProperties()
        |> Seq.iter(fun a -> Trace.WriteLine <| sprintf "%A" a.ProgrammaticName)
        Trace.IndentLevel <- 0
    member internal x.Element = root
    member internal x.Current = _current
    member x.Name with get() = _current.AutomationId

type TextBox (root, name) =
    inherit UIControlBase(root,name)
    let _pv = UIControlCommon.getPattern<ValuePattern> base.Element
    let _pt = UIControlCommon.getPattern<TextPattern> base.Element
    member x.Text
        with get () = _pt.DocumentRange.GetText(-1)
        and set v = _pv.SetValue v

type RadioButton (root, name) =
    inherit UIControlBase(root,name)
    let _selectionitem = UIControlCommon.getPattern<SelectionItemPattern> base.Element
    member x.IsChecked
        with get() = _selectionitem.Current.IsSelected
        and set v =
            if v then
                _selectionitem.Select()
            else
                _selectionitem.RemoveFromSelection()
//        ()
    member x.Click() =
        //TestApi - a library of Test APIs
        //http://testapi.codeplex.com/
        let rect = x.Current.BoundingRectangle
        let x = int rect.X + (int rect.Width / 2)
        let y = int rect.Y + (int rect.Height / 2)
        Mouse.MoveTo(new Point(x, y))
        Mouse.Click(MouseButton.Left)
        ()

type Button (root, name) =
    inherit UIControlBase(root,name)
    let _pi = UIControlCommon.getPattern<InvokePattern> base.Element
    member x.Click() = _pi.Invoke()

type CheckBox (root, name) =
    inherit UIControlBase(root,name)
    let _p = UIControlCommon.getPattern<TogglePattern> base.Element
    do
        ()
    member x.IsChecked
        with get() = _p.Current.ToggleState = ToggleState.On
        and set v =
            if x.IsChecked <> v then
                _p.Toggle()

