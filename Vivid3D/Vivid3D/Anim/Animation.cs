using Assimp.Unmanaged;
using Assimp;
using Vivid.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using Vivid.Scene;
using OpenTK.Mathematics;


namespace Vivid.Anim
{
    public class AssimpNodeData
    {
        public Matrix4 transformation;
        public string name = "";
        public int childrenCount = 0;
        public List<AssimpNodeData> children = new List<AssimpNodeData>();
      
              
      
    }

    public class Animation
    {
        public float m_Duration;
        private int m_TicksPerSecond;
        public string Name = "";
        public float Priority
        {
            get;
            set;
        }
        private List<Bone> m_Bones = new List<Bone>();
        private AssimpNodeData m_RootNode = new AssimpNodeData();
        private Dictionary<string, BoneInfo> m_BoneInfoMap = new Dictionary<string, BoneInfo>();
        public Animation() { }

        public Animation(Assimp.Scene scene, SkeletalEntity model)
        {
            var animation = scene.Animations[0];
            m_Duration = (float)animation.DurationInTicks;// mDuration;
            m_TicksPerSecond = (int)animation.TicksPerSecond;// m TicksPerSecond;
            ReadHeirarchyData(m_RootNode, scene.RootNode);
            ReadMissingBones(animation, model);
            Priority = 1.0f;
        }

        public Bone FindBone(string name)
        {
            foreach(var bone in m_Bones)
            {
                if (bone.GetBoneName() == name)
                {
                    return bone;
                }
            }
            return null;
        }

        public float GetTicksPerSecond()
        {
            return (float)m_TicksPerSecond;
        }

        public float GetDuration()
        {
            return (float)m_Duration;
        }

        public AssimpNodeData GetRootNode()
        {
            return m_RootNode;
        }

        public Dictionary<string, BoneInfo> GetBoneIDMap()
        {
            return m_BoneInfoMap;
        }

        private void ReadMissingBones(Assimp.Animation animation, SkeletalEntity model)
        {
            int size = animation.NodeAnimationChannels.Count;
           

            var boneInfoMap = model.GetBoneInfoMap();
            int boneCount = model.m_BoneCounter;

            for (int i = 0; i < size; i++)
            {
                var channel = animation.NodeAnimationChannels[i];
                string boneName = channel.NodeName;

                if (!boneInfoMap.ContainsKey(boneName))
                {
                    BoneInfo nbone = new BoneInfo();

                    boneInfoMap.Add(boneName, nbone);
                    boneInfoMap[boneName].id = boneCount;

                    boneCount++;
                    model.m_BoneCounter++;
                }

                m_Bones.Add(new Bone(channel.NodeName, boneInfoMap[channel.NodeName].id, channel));
            }

            m_BoneInfoMap = boneInfoMap;
          //model.m_BoneInfoMap = boneInfoMap;
        }
     
        private void ReadHeirarchyData(AssimpNodeData dest,Assimp.Node src)
        {
            //dest.name = src.Name;

            dest.name = src.Name;

            var tm = src.Transform;



            //tm = Matrix4.Identity;



            dest.transformation = Vivid.Importing.Conv.AssimpMatrixToGLMMatrix(src.Transform);
                
            dest.childrenCount = src.Children.Count;

            for (int i = 0; i < src.Children.Count; i++)
            {
                var newData = new AssimpNodeData();
                ReadHeirarchyData(newData, src.Children[i]);
                dest.children.Add(newData);
            }
        }
    }



}
