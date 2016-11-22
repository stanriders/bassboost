using System;
using System.ComponentModel;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
using Un4seen.Bass.AddOn.Enc;

// !! Вместе с программой обязательно должны лежать:
//       bass.dll
//       bass_fx.dll
//       bass_enc.dll
//       lame.exe

namespace basbust
{
    public partial class mainForm : Form
    {
        private int audioStream;            // основной поток
        private int fxStream;               // обработанный поток

        private string audioPath;           // путь к оригиналу

        private int distortion;             // хэндл дисторшена
        private int bassboost, bassboost2;  // хэндл эквалайзера

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // Подгружаем BASS
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_SPEAKERS, this.Handle))
            {
                BASS_INFO info = new BASS_INFO();
                Bass.BASS_GetInfo(info);
                debugInfo.Text += info + "\n" + BassFx.BASS_FX_GetVersion();

                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 200);
                Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, 1000);
            }
            else
            {
                MessageBox.Show(this, "BASS_Init() не сработал, басов не будет");
                Close();
            }
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            // Выбор файла
            openfile.ShowDialog();
        }

        private void openfile_FileOk(object sender, CancelEventArgs e)
        {
            if (openfile.FileName != String.Empty)
            {
                // Правим интерфейс
                selectButton.Text = "Заебись";
                selectButton.Enabled = false;

                audioPath = openfile.FileName;
                fileName.Text = audioPath;

                playButton.Enabled = true;
                saveButton.Enabled = true;

                audioStream = Bass.BASS_StreamCreateFile(audioPath, 0, 0, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_STREAM_PRESCAN);
                if (audioStream != 0)
                {
                    fxStream = BassFx.BASS_FX_TempoCreate(audioStream, BASSFlag.BASS_FX_FREESOURCE);
                    if (fxStream != 0)
                    {
                        // Вешаем эффекты до воиспроизведения, чтобы не было задержки
                        distortion = Bass.BASS_ChannelSetFX(fxStream, BASSFXType.BASS_FX_BFX_DISTORTION, -4);

                        BASS_BFX_DISTORTION dist = new BASS_BFX_DISTORTION();
                        dist.fDrive = 0.0f;
                        dist.fDryMix = 5.0f;
                        dist.fWetMix = 1.0f;
                        dist.fFeedback = -0.5f;
                        dist.fVolume = 0.1f;

                        Bass.BASS_FXSetParameters(distortion, dist);
                        // --
                        bassboost = Bass.BASS_ChannelSetFX(fxStream, BASSFXType.BASS_FX_DX8_PARAMEQ, -4);

                        BASS_DX8_PARAMEQ eq = new BASS_DX8_PARAMEQ();
                        eq.fBandwidth = 24;
                        eq.fCenter = 80;
                        eq.fGain = 0;

                        Bass.BASS_FXSetParameters(bassboost, eq);

                        bassboost2 = Bass.BASS_ChannelSetFX(fxStream, BASSFXType.BASS_FX_DX8_PARAMEQ, -4);
                        Bass.BASS_FXSetParameters(bassboost2, eq); return;
                    }
                }
                MessageBox.Show(this, "Чот нихуя");
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Прослушивание
            if (fxStream != 0)
            {
                if (Bass.BASS_ChannelIsActive(fxStream) == BASSActive.BASS_ACTIVE_PLAYING)
                {
                    Bass.BASS_ChannelStop(fxStream);
                    playButton.Text = "Заценить";
                }
                else
                {
                    Bass.BASS_ChannelPlay(fxStream, false);
                    playButton.Text = "Харэ";
                }

                return;
            }
            MessageBox.Show(this, "Чот нихуя");
        }

        private void distAmount_Scroll(object sender, EventArgs e)
        {
            // Изменение драйва и вета дисторшена
            BASS_BFX_DISTORTION dist = new BASS_BFX_DISTORTION();
            Bass.BASS_FXGetParameters(distortion, dist);

            dist.fDrive = distAmount.Value;
            dist.fWetMix = distAmount.Value / 3.0f;
            Bass.BASS_FXSetParameters(distortion, dist);
        }

        private void bassAmount_Scroll(object sender, EventArgs e)
        {
            // Изменения гейна эквалайзеров
            BASS_DX8_PARAMEQ eq = new BASS_DX8_PARAMEQ();
            Bass.BASS_FXGetParameters(bassboost, eq);

            eq.fGain = bassAmount.Value;
            Bass.BASS_FXSetParameters(bassboost, eq);
            Bass.BASS_FXSetParameters(bassboost2, eq);

            // Делаем кол-во Ы в лейбле равным бусту
            bassAmountLabel.Text = "БасЫ";
            for (int i = 1; i <= bassAmount.Value; i++)
            {
                bassAmountLabel.Text += "ы";
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Если аудио играет - выключаем
            if (Bass.BASS_ChannelIsActive(fxStream) == BASSActive.BASS_ACTIVE_PLAYING)
                Bass.BASS_ChannelStop(fxStream);

            saveFile.ShowDialog();
        }

        private void saveFile_FileOk(object sender, CancelEventArgs e)
        {
            // Энкодим в мп3
            // По идее для такого процесса желательно создать отдельный тред, чтобы не вешать интерфейс
            // Но так как процесс занимает буквально до 10 секунд, то проще оставить как есть
            int encoder = BassEnc.BASS_Encode_Start(fxStream, "lame -b128 - \"" + saveFile.FileName + "\"", 0, null, this.Handle);

            int[] encBuffer = new int[32767]; // буффер для записи

            long i = 0;
            while (i < Bass.BASS_ChannelGetLength(fxStream) - 1)
            {
                // Так как буфер в 1сек, то и записываем по одной секунде данных в проход
                Bass.BASS_ChannelSetPosition(fxStream, i, BASSMode.BASS_POS_BYTES);
                long len = Bass.BASS_ChannelSeconds2Bytes(fxStream, 1);
                Bass.BASS_ChannelUpdate(fxStream, 1000);

                BassEnc.BASS_Encode_Write(encoder, encBuffer, (int)len);
                i += len;

                debugInfo.Text = Bass.BASS_ErrorGetCode().ToString() + "\n" + len + "\n" + i + "\n" + Bass.BASS_ChannelGetLength(fxStream);
            }
            BassEnc.BASS_Encode_Stop(encoder);
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }
    }
}
