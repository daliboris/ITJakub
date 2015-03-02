using GalaSoft.MvvmLight;
using ITJakub.MobileApps.Client.SynchronizedReading.View.Control;

namespace ITJakub.MobileApps.Client.SynchronizedReading.ViewModel.Reading
{
    public class TextReaderViewModel : ViewModelBase
    {
        private int m_selectionStart;
        private int m_selectionLength;
        private int m_cursorPosition;
        private ReaderRichEditBox.Modes m_currentMode;
        private string m_documentRtf;
        private bool m_loading;

        public string DocumentRtf
        {
            get { return m_documentRtf; }
            set
            {
                m_documentRtf = value;
                RaisePropertyChanged();
            }
        }

        public int SelectionStart
        {
            get { return m_selectionStart; }
            set
            {
                m_selectionStart = value;
                RaisePropertyChanged();
            }
        }

        public int SelectionLength
        {
            get { return m_selectionLength; }
            set
            {
                m_selectionLength = value;
                RaisePropertyChanged();
            }
        }

        public int CursorPosition
        {
            get { return m_cursorPosition; }
            set
            {
                m_cursorPosition = value;
                RaisePropertyChanged();
            }
        }

        public ReaderRichEditBox.Modes CurrentMode
        {
            get { return m_currentMode; }
            set
            {
                m_currentMode = value;
                RaisePropertyChanged();
            }
        }

        public bool Loading
        {
            get { return m_loading; }
            set
            {
                m_loading = value;
                RaisePropertyChanged();
            }
        }
    }
}