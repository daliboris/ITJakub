﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using ITJakub.MobileApps.Client.Books.View.Control;
using ITJakub.MobileApps.Client.Fillwords.ViewModel;

namespace ITJakub.MobileApps.Client.Fillwords.View
{
    [TemplatePart(Name = "ContentElement", Type = typeof(ScrollViewer))]
    public class EditorRichEditBox : BindableRichEditBox
    {
        private ScrollViewer m_contentElement;

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            base.OnTapped(e);

            if (IsEditingEnabled)
                return;

            var point = e.GetPosition(this);
            var shiftedPoint = new Point(point.X, point.Y + m_contentElement.VerticalOffset);
            var textRange = Document.GetRangeFromPoint(shiftedPoint, PointOptions.ClientCoordinates);
            
            textRange.MoveStart(TextRangeUnit.Word, -1);
            textRange.MoveEnd(TextRangeUnit.Word, 1);

            Document.Selection.SetRange(textRange.StartPosition, textRange.EndPosition);

            SelectedText = textRange.Text.Trim();
            if (WordOptionsList != null && WordOptionsList.ContainsKey(textRange.StartPosition))
            {
                var key = textRange.StartPosition;
                
                SelectedOptions = new OptionsViewModel
                {
                    WordPosition = key,
                    List = new ObservableCollection<OptionViewModel>(WordOptionsList[key].List)
                };
            }
            else
            {
                SelectedOptions = new OptionsViewModel
                {
                    WordPosition = textRange.StartPosition,
                    List = new ObservableCollection<OptionViewModel>()
                };
            }

            Flyout.ShowAt(this);
            IsFlyoutOpen = true;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            m_contentElement = GetTemplateChild("ContentElement") as ScrollViewer;
        }

        public static readonly DependencyProperty FlyoutProperty = DependencyProperty.Register("Flyout", typeof (Flyout),
            typeof (EditorRichEditBox), new PropertyMetadata(null));

        public static readonly DependencyProperty WordOptionsListProperty = DependencyProperty.Register("WordOptionsList",
            typeof (Dictionary<int, OptionsViewModel>), typeof (EditorRichEditBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedOptionsProperty = DependencyProperty.Register(
            "SelectedOptions", typeof (OptionsViewModel), typeof (EditorRichEditBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.Register("SelectedText",
            typeof (string), typeof (EditorRichEditBox), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsFlyoutOpenProperty = DependencyProperty.Register("IsFlyoutOpen", typeof (bool), typeof (EditorRichEditBox), new PropertyMetadata(false, IsFlyoutOpenChanged));

        public static readonly DependencyProperty IsEditingEnabledProperty = DependencyProperty.Register("IsEditingEnabled", typeof (bool), typeof (EditorRichEditBox), new PropertyMetadata(false, IsEditingEnabledChanged));

        public Dictionary<int, OptionsViewModel> WordOptionsList
        {
            get { return (Dictionary<int, OptionsViewModel>)GetValue(WordOptionsListProperty); }
            set { SetValue(WordOptionsListProperty, value); }
        }

        public Flyout Flyout
        {
            get { return (Flyout) GetValue(FlyoutProperty); }
            set { SetValue(FlyoutProperty, value); }
        }

        public OptionsViewModel SelectedOptions
        {
            get { return (OptionsViewModel) GetValue(SelectedOptionsProperty); }
            set { SetValue(SelectedOptionsProperty, value); }
        }

        public string SelectedText
        {
            get { return (string) GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        public bool IsFlyoutOpen
        {
            get { return (bool) GetValue(IsFlyoutOpenProperty); }
            set { SetValue(IsFlyoutOpenProperty, value); }
        }

        public bool IsEditingEnabled
        {
            get { return (bool) GetValue(IsEditingEnabledProperty); }
            set { SetValue(IsEditingEnabledProperty, value); }
        }

        private static void IsFlyoutOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editBox = d as EditorRichEditBox;
            if (editBox == null)
                return;
            
            var isOpen = (bool) e.NewValue;
            if (!isOpen)
                editBox.Flyout.Hide();
        }


        private static void IsEditingEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editBox = d as EditorRichEditBox;
            if (editBox == null)
                return;

            var isEnabled = (bool)e.NewValue;
            editBox.IsReadOnly = !isEnabled;
        }
    }
}
