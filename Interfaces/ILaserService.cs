using TLCOnline.Models;

namespace TLCOnline.Interfaces
{
    public interface ILaserService
    {
        List<LaserModel> GetAllLasers();
        void AddLaser(LaserModel model);
        LaserModel GetLaserById(string id);
        void UpdateLaser(LaserModel model);
        void DeleteLaser(string id);
        bool CheckIfLaserNameExists(string laserName);
    }
}