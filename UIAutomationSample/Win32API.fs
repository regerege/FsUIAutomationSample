namespace UIAutomationSample

open System
open System.Runtime.InteropServices
open System.Runtime.InteropServices.ComTypes
open System.Security
open System.Text

module Win32API =
    [<DllImportAttribute("user32.dll", EntryPoint="mouse_event")>]
    extern unit W32MouseEvent(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo)

    let private MouseEventLeftDown = 0x0002u;
    let private MouseEventLeftUp = 0x0004u;
    let private MouseEventRightDown = 0x0008u;
    let private MouseEventRightUp = 0x00010u;

    let Click(x,y) =
        W32MouseEvent(MouseEventLeftDown, x, y, 0u, IntPtr.Zero)
        W32MouseEvent(MouseEventLeftUp, x, y, 0u, IntPtr.Zero)
