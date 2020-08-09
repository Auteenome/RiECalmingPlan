using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RiECalmingPlan.PhotoPicker {
    public interface IPhotoPickerService {
        Task<Stream> GetImageStreamAsync();
    }
}
