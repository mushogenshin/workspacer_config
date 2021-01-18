#r "C:\Program Files\workspacer\workspacer.Shared.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\Program Files\workspacer\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"

using System;
using workspacer;
using workspacer.Bar;
using workspacer.ActionMenu;
using workspacer.FocusIndicator;



Action<IConfigContext> doConfig = (context) =>
{
    context.AddBar();
    context.AddFocusIndicator();
    var actionMenu = context.AddActionMenu();

    // MULTIPLE MONITORS
    context.WorkspaceContainer.CreateWorkspaces("one", "2drawing", "threeD", "remote4", "five", "code6", "team7", "log8", "web9");

    // SINGLE MONITOR
    // var sticky = new StickyWorkspaceContainer(context, StickyWorkspaceIndexMode.Local);
    // context.WorkspaceContainer = sticky; 

    // sticky.CreateWorkspace("one", new VertLayoutEngine());
    // sticky.CreateWorkspace("two", new VertLayoutEngine());
    // sticky.CreateWorkspaces("one", "two");
    // sticky.CreateWorkspaces("three", "four", "five", "six", "team7", "log8", "web9");

    // CUSTOM FILTER
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Snipping"));
    // COPYING
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Discovered"));
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("% complete"));
    // OWN DIALOGS
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Info"));
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Warning"));
    // OWN APPS
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Swiss Army Knife"));  // Anastomia
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Asset Hunter"));  // Anastomia
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("MVP"));  // Anastomia
    
    // CUSTOM ROUTE
    // 2D
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Mischief") ? context.WorkspaceContainer["2drawing"] : null);

    // 3D
    // ZBRUSH
    context.WindowRouter.AddRoute((window) => window.Title.Contains("ZBrush 2021") ? context.WorkspaceContainer["threeD"] : null);
    // HOUDINI
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Houdini") ? context.WorkspaceContainer["threeD"] : null);
    // MAYA
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Autodesk Maya") ? context.WorkspaceContainer["threeD"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Output Window") ? context.WorkspaceContainer["threeD"] : null);
    // REMOTE
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Remote Desktop Connection") ? context.WorkspaceContainer["remote4"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("FortiClient") ? context.WorkspaceContainer["remote4"] : null);
    // CODING
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Visual Studio Code") ? context.WorkspaceContainer["code6"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Notepad++") ? context.WorkspaceContainer["code6"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("MongoDB Compass") ? context.WorkspaceContainer["code6"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("TOOL BOARD") ? context.WorkspaceContainer["code6"] : null);
    // CHAT
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Discord") ? context.WorkspaceContainer["team7"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Skype") ? context.WorkspaceContainer["team7"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Microsoft Teams") ? context.WorkspaceContainer["team7"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Outlook") ? context.WorkspaceContainer["team7"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Reminder") ? context.WorkspaceContainer["team7"] : null);
    // POWER OPTIONS
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Power Options") ? context.WorkspaceContainer["log8"] : null);
    // WACOM
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Wacom Desktop Center") ? context.WorkspaceContainer["log8"] : null);
    // VLC HACK
    context.WindowRouter.AddRoute((window) => window.Title.Contains("ptexTurntables") ? context.WorkspaceContainer["log8"] : null);
    // NOTION
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Notion") ? context.WorkspaceContainer["log8"] : null);
    // WEB
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Google Chrome") ? context.WorkspaceContainer["web9"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Mozilla Firefox") ? context.WorkspaceContainer["web9"] : null);

    // CUSTOM KEYBINDS
    // Modified from source at https://github.com/rickbutton/workspacer/blob/e58439adaf5f2ea0123a499467c115698adb107b/src/workspacer/Keybinds/KeybindManager.cs

    KeyModifiers mod1 = KeyModifiers.Win;
    KeyModifiers mod2 = KeyModifiers.Alt;

    context.Keybinds.UnsubscribeAll();

    // re-create SubscribeDefaults

    context.Keybinds.Subscribe(MouseEvent.LButtonDown,
        () => context.Workspaces.SwitchFocusedMonitorToMouseLocation());

    context.Keybinds.Subscribe(mod1 | mod2, Keys.Escape,
        () => context.Enabled = !context.Enabled, "toggle enable/disable");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.C,
        () => context.Workspaces.FocusedWorkspace.CloseFocusedWindow(), "close focused window");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.Space,
        () => context.Workspaces.FocusedWorkspace.NextLayoutEngine(), "next layout");

    // context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.Space,
    //     () => context.Workspaces.FocusedWorkspace.PreviousLayoutEngine(), "previous layout");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.N,
        () => context.Workspaces.FocusedWorkspace.ResetLayout(), "reset layout");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.J,
        () => context.Workspaces.FocusedWorkspace.FocusNextWindow(), "focus next window");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.K,
        () => context.Workspaces.FocusedWorkspace.FocusPreviousWindow(), "focus previous window");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.M,
        () => context.Workspaces.FocusedWorkspace.FocusPrimaryWindow(), "focus primary window");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.Enter,
        () => context.Workspaces.FocusedWorkspace.SwapFocusAndPrimaryWindow(), "swap focus and primary window");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.J,
        () => context.Workspaces.FocusedWorkspace.SwapFocusAndNextWindow(), "swap focus and next window");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.K,
        () => context.Workspaces.FocusedWorkspace.SwapFocusAndPreviousWindow(), "swap focus and previous window");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.H,
        () => context.Workspaces.FocusedWorkspace.ShrinkPrimaryArea(), "shrink primary area");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.L,
        () => context.Workspaces.FocusedWorkspace.ExpandPrimaryArea(), "expand primary area");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.Oemcomma,
        () => context.Workspaces.FocusedWorkspace.IncrementNumberOfPrimaryWindows(), "increment # primary windows");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.OemPeriod,
        () => context.Workspaces.FocusedWorkspace.DecrementNumberOfPrimaryWindows(), "decrement # primary windows");

    // context.Keybinds.Subscribe(mod1 | mod2, Keys.T,
    //     () => context.Windows.ToggleFocusedWindowTiling(), "toggle tiling for focused window");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.Q, context.Quit, "quit workspacer");
    
    context.Keybinds.Subscribe(mod1 | mod2, Keys.Q, context.Restart, "restart workspacer");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D1,
        () => context.Workspaces.SwitchToWorkspace(0), "switch to workspace 1");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D2,
        () => context.Workspaces.SwitchToWorkspace(1), "switch to workspace 2");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D3,
        () => context.Workspaces.SwitchToWorkspace(2), "switch to workspace 3");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D4,
        () => context.Workspaces.SwitchToWorkspace(3), "switch to workspace 4");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D5,
        () => context.Workspaces.SwitchToWorkspace(4), "switch to workspace 5");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D6,
        () => context.Workspaces.SwitchToWorkspace(5), "switch to workspace 6");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D7,
        () => context.Workspaces.SwitchToWorkspace(6), "switch to workspace 7");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D8,
        () => context.Workspaces.SwitchToWorkspace(7), "switch to workspace 8");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.D9,
        () => context.Workspaces.SwitchToWorkspace(8), "switch to workpsace 9");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.Left,
        () => context.Workspaces.SwitchToPreviousWorkspace(), "switch to previous workspace");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.Right,
        () => context.Workspaces.SwitchToNextWorkspace(), "switch to next workspace");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.Oemtilde,
        () => context.Workspaces.SwitchToLastFocusedWorkspace(), "switch to last focused workspace");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.W,
        () => context.Workspaces.SwitchFocusedMonitor(0), "focus monitor 1");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.E,
        () => context.Workspaces.SwitchFocusedMonitor(1), "focus monitor 2");

    context.Keybinds.Subscribe(mod1 | mod2, Keys.R,
        () => context.Workspaces.SwitchFocusedMonitor(2), "focus monitor 3");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.W,
        () => context.Workspaces.MoveFocusedWindowToMonitor(0), "move focused window to monitor 1");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.E,
        () => context.Workspaces.MoveFocusedWindowToMonitor(1), "move focused window to monitor 2");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.R,
        () => context.Workspaces.MoveFocusedWindowToMonitor(2), "move focused window to monitor 3");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D1,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(0), "switch focused window to workspace 1");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D2,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(1), "switch focused window to workspace 2");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D3,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(2), "switch focused window to workspace 3");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D4,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(3), "switch focused window to workspace 4");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D5,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(4), "switch focused window to workspace 5");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D6,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(5), "switch focused window to workspace 6");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D7,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(6), "switch focused window to workspace 7");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D8,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(7), "switch focused window to workspace 8");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.D9,
        () => context.Workspaces.MoveFocusedWindowToWorkspace(8), "switch focused window to workspace 9");

    // context.Keybinds.Subscribe(mod1 | mod2, Keys.O,
    //     () => context.Windows.DumpWindowDebugOutput(), "dump debug info to console for all windows");

    // context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.O,
    //     () => context.Windows.DumpWindowUnderCursorDebugOutput(), "dump debug info to console for window under cursor");

    context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.I,
        () => context.ToggleConsoleWindow(), "toggle debug console");

    // context.Keybinds.Subscribe(mod1 | mod2 | KeyModifiers.LShift, Keys.Oem2,
    //     () => ShowKeybindDiateam7(), "open keybind window");
    
};
return doConfig;