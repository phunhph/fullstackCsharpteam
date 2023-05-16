using Emgu.CV.CvEnum;
using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace fullstackCsharp.Controllers
{
    public class BefaceController : Controller
    {
        private const string TrainedDataPath = @"C:\Users\Huu Phu\OneDrive - Đại học FPT- FPT University\Desktop\data.xml";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FaceRecognition()
        {
            // Tạo đối tượng VideoCapture để lấy ảnh từ camera
            using (var capture = new VideoCapture(0))
            {
                // Tạo đối tượng nhận diện khuôn mặt
                var faceRecognizer = EigenFaceRecognizer.Create();

                // Đọc dữ liệu khuôn mặt đã được train
                faceRecognizer.Read(TrainedDataPath);

                // Vòng lặp để liên tục lấy ảnh từ camera và thực hiện nhận diện khuôn mặt
                while (true)
                {
                    // Đọc ảnh từ camera và gán kết quả vào đối tượng Mat
                    using (var frame = new Mat())
                    {
                        capture.Read(frame);

                        // Chuyển ảnh đọc được sang ảnh grayscale
                        using (var grayFrame = new Mat())
                        {
                            // Khởi tạo một bộ phân loại cascade cho nhận diện khuôn mặt sử dụng tệp XML "haarcascade_frontalface_default.xml"
                            var faceCascade = new CascadeClassifier("C:\\Users\\Huu Phu\\OneDrive - Đại học FPT- FPT University\\Desktop\\data.xml");
                            // Chuyển đổi khung hình từ không gian màu BGR sang màu xám
                            Cv2.CvtColor(frame, grayFrame, ColorConversionCodes.BGR2GRAY);
                            // Cải thiện độ tương phản của khung hình màu xám
                            Cv2.EqualizeHist(grayFrame, grayFrame);
                            // vẽ khung nhận diện khuôn mặt
                            var faces = faceCascade.DetectMultiScale(grayFrame, 1.3, 5, HaarDetectionType.DoRoughSearch, new OpenCvSharp.Size(30, 30));
                            foreach (var faceRect in faces)
                            {
                                Cv2.Rectangle(frame, faceRect, new Scalar(0, 255, 0), 2);
                            }

                            // Chuyển ảnh Mat sang ảnh bitmap
                            var bitmap = BitmapConverter.ToBitmap(frame);

                            // Trả về ảnh đã được nhận diện khuôn mặt
                            using (var ms = new MemoryStream())
                            {
                                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                return File(ms.ToArray(), "image/jpeg");
                            }
                        }
                    }
                }
            }
        }
    }
}
