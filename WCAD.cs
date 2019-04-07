using System;
using System.Runtime.InteropServices;

namespace Private.BlurClear
{
    #region enum

    public enum WindowCompositonAttribute
    {
        WCA_UNDEFINED = 0,
        WCA_NCRENDERING_ENABLED = 1,
        WCA_NCRENDERING_POLICY = 2,
        WCA_TRANSITIONS_FORCEDISABLED = 3,
        WCA_ALLOW_NCPAINT = 4,
        WCA_CAPTION_BUTTON_BOUNDS = 5,
        WCA_NONCLIENT_RTL_LAYOUT = 6,
        WCA_FORCE_ICONIC_REPRESENTATION = 7,
        WCA_EXTENDED_FRAME_BOUNDS = 8,
        WCA_HAS_ICONIC_BITMAP = 9,
        WCA_THEME_ATTRIBUTES = 10,
        WCA_NCRENDERING_EXILED = 11,
        WCA_NCADORNMENTINFO = 12,
        WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,
        WCA_VIDEO_OVERLAY_ACTIVE = 14,
        WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15,
        WCA_DISALLOW_PEEK = 16,
        WCA_CLOAK = 17,
        WCA_CLOAKED = 18,
        WCA_ACCENT_POLICY = 19,
        WCA_FREEZE_REPRESENTATION = 20,
        WCA_EVER_UNCLOAKED = 21,
        WCA_VISUAL_OWNER = 22,
        WCA_LAST = 23
    }

    public enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    #endregion

    #region struct

    public struct AccentPolicy
    {
        public AccentState AccentState { get; set; }

        public int AccentFlags { get; set; }

        public int GradientColor { get; set; }

        public int AnimationId { get; set; }

    }

    public struct WindowCompositonAttributeData
    {
        public WindowCompositonAttribute Attribute { get; set; }

        public System.IntPtr Data { get; set; }

        public int DataSize { get; set; }


        public static WindowCompositonAttributeData 透明
        {
            get
            {
                AccentPolicy accentPolicy = new AccentPolicy();
                accentPolicy.AccentState = AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT;
                accentPolicy.AccentFlags = 2;
                accentPolicy.AnimationId = 0;
                accentPolicy.GradientColor = 0;

                int size = System.Runtime.InteropServices.Marshal.SizeOf(accentPolicy);
                IntPtr accentPtr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(accentPolicy, accentPtr, false);

                WindowCompositonAttributeData data = new WindowCompositonAttributeData();
                data.Attribute = WindowCompositonAttribute.WCA_ACCENT_POLICY;
                data.DataSize = size;
                data.Data = accentPtr;

                return data;
            }
        }

        public static WindowCompositonAttributeData 模糊
        {
            get
            {
                AccentPolicy accentPolicy = new AccentPolicy();
                accentPolicy.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
                accentPolicy.AccentFlags = 0;
                accentPolicy.AnimationId = 0;
                accentPolicy.GradientColor = 0;

                int size = Marshal.SizeOf(accentPolicy);
                IntPtr accentPtr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(accentPolicy, accentPtr, false);

                WindowCompositonAttributeData data = new WindowCompositonAttributeData();
                data.Attribute = WindowCompositonAttribute.WCA_ACCENT_POLICY;
                data.DataSize = size;
                data.Data = accentPtr;

                return data;
            }
        }
    }

    #endregion
}
