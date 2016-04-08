using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TreasureHunter
{
    class CameraManager
    {
        static CameraManager cameraManager;
        List<Camera> cameras;
        int current;
        CameraManager(){
            cameras = new List<Camera>();
            current = 0;
        }

        static public CameraManager Instance()
        {
            if (cameraManager == null){
                cameraManager = new CameraManager();
            }
            return cameraManager;
        }

        public void Update(){
            if (cameras.Count >= 0)
            {
                cameras.ElementAt<Camera>(current).Update();
            }
        }

        public void AddCamera(Camera camera) {
            cameras.Add(camera);
        }

        public void SelectCamera(int index) {
           current = (int)MathHelper.Clamp(index, 0, cameras.Count-1);
        }

        public Camera GetCamera() {
            return cameras.ElementAt<Camera>(current);
        }
    }
}
