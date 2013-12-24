using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExpandableControl
{
    /// <summary>
    /// 手势类型
    /// </summary>
    public enum ExpandablePanelGestureType
    {
        /// <summary>
        /// 不触发手势
        /// </summary>
        None = 1 >> 1,
        /// <summary>
        /// 长按触发
        /// </summary>
        Hold = 1 >> 0,
        /// <summary>
        /// 点击触发
        /// </summary>
        Tap = 1 >> -1,
        /// <summary>
        /// 长按点击均触发
        /// </summary>
        Both = Hold | Tap
    }

    [TemplatePart(Name = ContainerPanelName, Type = typeof(StackPanel))]
    [TemplatePart(Name = DisplayPanelName, Type = typeof(Grid))]
    [TemplatePart(Name = ExpandPanelName, Type = typeof(Grid))]
    [TemplatePart(Name = DisplayContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = IgnoreContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ExpandContentPresenterName, Type = typeof(ContentPresenter))]
    public class ExpandablePanel : Control
    {

        #region Template

        private const string ContainerPanelName = "ContainerPanel";
        private const string DisplayPanelName = "DisplayPanel";
        private const string ExpandPanelName = "ExpandPanel";
        private const string DisplayContentPresenterName = "DisplayContentPresenter";
        private const string IgnoreContentPresenterName = "IgnoreContentPresenter";
        private const string ExpandContentPresenterName = "ExpandContentPresenter";

        private StackPanel _containerPanel;
        private Grid _displayPanel;
        private Grid _expandPanel;
        private ContentPresenter _displayContentPresenter;
        private ContentPresenter _ignoreContentPresenter;
        private ContentPresenter _expandContentPresenter;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _containerPanel = GetTemplateChild(ContainerPanelName) as StackPanel;
            _displayPanel = GetTemplateChild(DisplayPanelName) as Grid;
            _expandPanel = GetTemplateChild(ExpandPanelName) as Grid;
            _displayContentPresenter = GetTemplateChild(DisplayContentPresenterName) as ContentPresenter;
            _ignoreContentPresenter = GetTemplateChild(IgnoreContentPresenterName) as ContentPresenter;
            _expandContentPresenter = GetTemplateChild(ExpandContentPresenterName) as ContentPresenter;
            if (_displayContentPresenter != null)
            {
                _displayContentPresenter.Tap += DisplayContentPresenter_Tap;
                _displayContentPresenter.Hold += DisplayContentPresenter_Hold;
            }
        }

        #endregion

        private static readonly Dictionary<string, WeakReference<ExpandablePanel>> LastExpandedTarget = new Dictionary<string, WeakReference<ExpandablePanel>>();

        public ExpandablePanel()
        {
            DefaultStyleKey = typeof (ExpandablePanel);
        }

        #region 依赖属性

        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register("GroupName", typeof (string), typeof (ExpandablePanel), new PropertyMetadata(default(string)));

        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName
        {
            get { return (string) GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        public static readonly DependencyProperty DisplayTemplateProperty = DependencyProperty.Register("DisplayTemplate", typeof (ControlTemplate), typeof (ExpandablePanel), new PropertyMetadata(default(ControlTemplate)));

        /// <summary>
        /// 展示区域模板
        /// </summary>
        public ControlTemplate DisplayTemplate
        {
            get { return (ControlTemplate) GetValue(DisplayTemplateProperty); }
            set { SetValue(DisplayTemplateProperty, value); }
        }

        public static readonly DependencyProperty DisplayContentProperty = DependencyProperty.Register("DisplayContent", typeof (object), typeof (ExpandablePanel), new PropertyMetadata(default(object)));

        /// <summary>
        /// 展示区域内容
        /// </summary>
        public object DisplayContent
        {
            get { return GetValue(DisplayContentProperty); }
            set { SetValue(DisplayContentProperty, value); }
        }

        public static readonly DependencyProperty ExpandTemplateProperty = DependencyProperty.Register("ExpandTemplate", typeof (ControlTemplate), typeof (ExpandablePanel), new PropertyMetadata(default(ControlTemplate)));

        /// <summary>
        /// 展开区域模板
        /// </summary>
        public ControlTemplate ExpandTemplate
        {
            get { return (ControlTemplate) GetValue(ExpandTemplateProperty); }
            set { SetValue(ExpandTemplateProperty, value); }
        }

        public static readonly DependencyProperty ExpandContentProperty = DependencyProperty.Register("ExpandContent", typeof (object), typeof (ExpandablePanel), new PropertyMetadata(default(object)));

        /// <summary>
        /// 展开区域内容
        /// </summary>
        public object ExpandContent
        {
            get { return GetValue(ExpandContentProperty); }
            set { SetValue(ExpandContentProperty, value); }
        }

        public static readonly DependencyProperty IgnoreTemplateProperty = DependencyProperty.Register("IgnoreTemplate", typeof (ControlTemplate), typeof (ExpandablePanel), new PropertyMetadata(default(ControlTemplate)));

        /// <summary>
        /// 手势忽略区域模板
        /// </summary>
        public ControlTemplate IgnoreTemplate
        {
            get { return (ControlTemplate) GetValue(IgnoreTemplateProperty); }
            set { SetValue(IgnoreTemplateProperty, value); }
        }

        public static readonly DependencyProperty IgnoreContentProperty = DependencyProperty.Register("IgnoreContent", typeof (object), typeof (ExpandablePanel), new PropertyMetadata(default(object)));

        /// <summary>
        /// 手势忽略区域内容
        /// </summary>
        public object IgnoreContent
        {
            get { return GetValue(IgnoreContentProperty); }
            set { SetValue(IgnoreContentProperty, value); }
        }

        public static readonly DependencyProperty GestureTypeProperty = DependencyProperty.Register("GestureType", typeof (ExpandablePanelGestureType), typeof (ExpandablePanel), new PropertyMetadata(default(ExpandablePanelGestureType)));

        public ExpandablePanelGestureType GestureType
        {
            get { return (ExpandablePanelGestureType) GetValue(GestureTypeProperty); }
            set { SetValue(GestureTypeProperty, value); }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 手势触发了展开或关闭
        /// </summary>
        private void GestureFired()
        {
            
        }

        private void DisplayContentPresenter_Hold(object sender, GestureEventArgs e)
        {
            if ((GestureType & ExpandablePanelGestureType.Hold) != ExpandablePanelGestureType.Hold)
            {
                return;
            }
            GestureFired();
        }

        private void DisplayContentPresenter_Tap(object sender, GestureEventArgs e)
        {
            if ((GestureType & ExpandablePanelGestureType.Tap) != ExpandablePanelGestureType.Tap)
            {
                return;
            }
            GestureFired();
        }


        #endregion

    }
}
