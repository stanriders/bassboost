using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ManagedBass;
using ManagedBass.Enc;
using ManagedBass.Fx;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace basbust_next
{
    public partial class MainWindow : Window
    {
        private int audioStream;
        private int fxStream;

        private PeakEQ bassboost;

        private long audioLength;
        private const int buffer_length = 200;  // ms

        private EncodingProgress encodingDialog;
        private CancellationTokenSource cancelEncoding;

        private string saveFilename;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                if (Bass.Init())
                {
                    Bass.Configure(Configuration.UpdatePeriod, 200);
                    Bass.Configure(Configuration.PlaybackBufferLength, buffer_length);
                    Bass.Configure(Configuration.GlobalStreamVolume, 10000);
                }
                else
                {
                    MessageBox.Show(this, "BASS_Init() не сработал, басов не будет");
                    Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, $"BASS_Init() не сработал, басов не будет. {e.Message}");
                Close();
            }
        }
        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            //play
            if (fxStream != 0)
            {
                if (Bass.ChannelIsActive(fxStream) == PlaybackState.Playing)
                {
                    StopPlayback();
                }
                else
                {
                    StartPlayback();
                }
            }
            else
            {
                playButton.IsEnabled = false;
            }
        }

        private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "mp3",
                Filter = "Лютое басилово (*.mp3)|*.mp3"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                saveFilename = saveFileDialog.FileName;
                encodingDialog = new EncodingProgress();
                await DialogHost.Show(encodingDialog, "RootDialog", EncodingStart, EncodingFinish);
            }
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                LoadFile(openFileDialog.FileName);
            }
        }

        private void LoadFile(string filename)
        {
            if (filename != string.Empty)
            {
                if (Bass.ChannelIsActive(fxStream) == PlaybackState.Playing)
                    StopPlayback();

                Bass.StreamFree(audioStream);
                Bass.StreamFree(fxStream);

                audioStream = Bass.CreateStream(filename, 0, 0, BassFlags.Decode | BassFlags.Prescan);
                audioLength = Bass.ChannelGetLength(audioStream);
                if (audioStream != 0 && Bass.LastError == Errors.OK)
                {
                    fxStream = BassFx.TempoCreate(audioStream, BassFlags.FxFreeSource);
                    if (fxStream != 0 && Bass.LastError == Errors.OK)
                    {
                        bassboost = new PeakEQ(fxStream, 0, 3);
                        bassboost.UpdateBand(bassboost.AddBand(30), bassAmount.Value * 2);
                        bassboost.UpdateBand(bassboost.AddBand(50), bassAmount.Value);

                        playButton.IsEnabled = true;
                        saveButton.IsEnabled = true;

                        return;
                    }
                }

                MessageBox.Show(this, "Чот нихуя");
            }
        }
        private async void EncodingStart(object o, DialogOpenedEventArgs args)
        {
            StopPlayback();

            using (cancelEncoding = new CancellationTokenSource())
            {
                await Task.Run(() =>
                {
                    Bass.ChannelSetPosition(fxStream, 0);

                    int encoder = BassEnc_Mp3.Start(fxStream, "-b 192", EncodeFlags.Default, saveFilename);

                    long i = 0;
                    while (i < audioLength - 1)
                    {
                        if (cancelEncoding.Token.IsCancellationRequested)
                            return;

                        Bass.ChannelSetPosition(fxStream, i);
                        Bass.ChannelUpdate(fxStream, buffer_length);

                        encodingDialog.Dispatcher?.Invoke(() =>
                        {
                            encodingDialog.UpdateProgress((double) i / audioLength * 100.0);
                        });

                        long len = Bass.ChannelSeconds2Bytes(fxStream, buffer_length / 1000.0);
                        i += len;
                    }

                    BassEnc.EncodeStop(encoder);
                }, cancelEncoding.Token);
            }

            dialogHost.IsOpen = false;

        }

        private void EncodingFinish(object o, DialogClosingEventArgs args)
        {
            if(args.Parameter as string == "Cancel")
                cancelEncoding.Cancel();
        }

        private void StartPlayback()
        {
            Bass.ChannelSetPosition(fxStream, audioLength / 3); // start from second third of the track
            Bass.ChannelPlay(fxStream);
            playIcon.Kind = PackIconKind.Pause;
            ButtonProgressAssist.SetIsIndicatorVisible(playButton, true);
        }

        private void StopPlayback()
        {
            Bass.ChannelStop(fxStream);
            playIcon.Kind = PackIconKind.Play;
            ButtonProgressAssist.SetIsIndicatorVisible(playButton, false);
        }

        private void BassAmount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.OldValue != e.NewValue && bassboost != null)
            {
                bassboost.UpdateBand(0, e.NewValue * 2);
                bassboost.UpdateBand(1, e.NewValue);
            }
        }

        private void VolumeAmount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.OldValue != e.NewValue)
            {
                Bass.Configure(Configuration.GlobalStreamVolume, Convert.ToInt32(e.NewValue) * 100);
            }
        }
    }
}
