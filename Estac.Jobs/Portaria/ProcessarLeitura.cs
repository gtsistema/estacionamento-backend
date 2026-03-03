using System;
using System.Collections.Generic;
using OpenCvSharp;
using Tesseract;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;

namespace Estac.Jobs.Portaria
{
    public class ProcessarLeitura
    {
        private readonly Regex _placaRegex =
            new(@"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$|^[A-Z]{3}[0-9]{4 }$",
                RegexOptions.Compiled);

        public async Task ExecuteAsync()
        {
            using var capture = new VideoCapture(0);
            using var ocr = new TesseractEngine(@"./tessdata", "por", EngineMode.Default);

            if (!capture.IsOpened())
            {
                Console.WriteLine("Webcam não encontrada.");
                return;
            }

            Console.WriteLine("Iniciando leitura de placas...");

            while (true)
            {
                using var frame = new Mat();
                capture.Read(frame);

                if (frame.Empty())
                    continue;

                using var gray = new Mat();
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                Cv2.GaussianBlur(gray, gray, new Size(5, 5), 0);
                Cv2.Canny(gray, gray, 100, 200);

                var contours = Cv2.FindContoursAsArray(
                    gray,
                    RetrievalModes.Tree,
                    ContourApproximationModes.ApproxSimple);

                foreach (var contour in contours)
                {
                    var rect = Cv2.BoundingRect(contour);

                    // Proporção típica de placa
                    double ratio = rect.Width / (double)rect.Height;

                    if (ratio > 2 && ratio < 6 && rect.Width > 100)
                    {
                        using var possiblePlate = new Mat(frame, rect);
                        using var plateGray = new Mat();

                        Cv2.CvtColor(possiblePlate, plateGray, ColorConversionCodes.BGR2GRAY);
                        Cv2.Threshold(plateGray, plateGray, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

                        using var pix = PixConverter.ToPix(plateGray.ToBitmap());
                        using var page = ocr.Process(pix);

                        var text = page.GetText()
                                       .Trim()
                                       .Replace(" ", "")
                                       .Replace("-", "")
                                       .ToUpper();

                        if (_placaRegex.IsMatch(text))
                        {
                            Console.WriteLine($"Placa detectada: {text} - {DateTime.Now}");

                            // Aqui você pode salvar no banco
                            await SalvarPlacaAsync(text);
                        }
                    }
                }

                await Task.Delay(50); // reduz uso de CPU
            }
        }

        private Task SalvarPlacaAsync(string placa)
        {
            // Implementar persistência
            return Task.CompletedTask;
        }
    }
}
