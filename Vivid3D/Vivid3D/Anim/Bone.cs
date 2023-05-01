using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp.Unmanaged;
using Assimp;
using Vivid.Maths;
using OpenTK.Mathematics;

namespace Vivid.Anim
{


    struct KeyPosition
    {
        public Vector3 position;
        public float timeStamp;

        public KeyPosition(Vector3 position, float timeStamp = 0.0f)
        {
            this.position = position;
            this.timeStamp = timeStamp;
        }
    }

    public class KeyRotation
    {
        public OpenTK.Mathematics.Quaternion orientation;
        public float timeStamp;

        public KeyRotation(OpenTK.Mathematics.Quaternion orientation, float timeStamp = 0.0f)
        {
            this.orientation = orientation;
            this.timeStamp = timeStamp;
        }
    }

    struct KeyScale
    {
        public Vector3 scale;
        public float timeStamp;

        public KeyScale(Vector3 scale, float timeStamp = 0.0f)
        {
            this.scale = scale;
            this.timeStamp = timeStamp;
        }
    }


    public class Bone
    {
        private List<KeyPosition> m_Positions;
        private List<KeyRotation> m_Rotations;
        private List<KeyScale> m_Scales;
        private int m_NumPositions;
        private int m_NumRotations;
        private int m_NumScalings;

        private Matrix4 m_LocalTransform;
        private string m_Name;
        private int m_ID;

        public Bone()
        {
            m_Positions = new List<KeyPosition>();
            m_Rotations = new List<KeyRotation>();
            m_Scales = new List<KeyScale>();
        }

        /*reads keyframes from aiNodeAnim*/
        public Bone(string name, int ID, Assimp.NodeAnimationChannel channel)
        {
            m_Name = name;
            m_ID = ID;
            m_LocalTransform = Matrix4.Identity;

            m_NumPositions = channel.PositionKeys.Count;
            m_Positions = new List<KeyPosition>();
            for (int positionIndex = 0; positionIndex < m_NumPositions; ++positionIndex)
            {
                Vector3D aiPosition = channel.PositionKeys[positionIndex].Value;
                float timeStamp = (float)channel.PositionKeys[positionIndex].Time;
                KeyPosition data = new KeyPosition();
                data.position = new Vector3(aiPosition.X, aiPosition.Y, aiPosition.Z);
                // data.position.x = -data.position.x;
                data.timeStamp = timeStamp;
                m_Positions.Add(data);
            }

            m_NumRotations = channel.RotationKeys.Count;
            m_Rotations = new List<KeyRotation>();
            for (int rotationIndex = 0; rotationIndex < m_NumRotations; ++rotationIndex)
            {
                Assimp.Quaternion aiOrientation = channel.RotationKeys[rotationIndex].Value;
                float timeStamp = (float)channel.RotationKeys[rotationIndex].Time;
                KeyRotation data = new KeyRotation(new OpenTK.Mathematics.Quaternion(aiOrientation.X, aiOrientation.Y, aiOrientation.Z, aiOrientation.W),timeStamp);
              
                m_Rotations.Add(data);
            }

            m_NumScalings = channel.ScalingKeys.Count;
            m_Scales = new List<KeyScale>();
            for (int keyIndex = 0; keyIndex < m_NumScalings; ++keyIndex)
            {
                Vector3D scale = channel.ScalingKeys[keyIndex].Value;
                float timeStamp = (float)channel.ScalingKeys[keyIndex].Time;
                KeyScale data = new KeyScale();
                data.scale = new Vector3(scale.X, scale.Y, scale.Z);// GetGLMVec(scale);
                data.timeStamp = timeStamp;
                m_Scales.Add(data);
            }
        }

        /*interpolates  b/w positions,rotations & scaling keys based on the current time of
        the animation and prepares the local transformation matrix by combining all keys
        transformations*/
        public void Update(float animationTime)
        {
            Matrix4 translation = InterpolatePosition(animationTime);
            Matrix4 rotation = InterpolateRotation(animationTime);
            Matrix4 scale = InterpolateScaling(animationTime);
            //m_LocalTransform = translation * rotation;//*scale;
            m_LocalTransform = rotation * translation;
        }

        public Matrix4 GetLocalTransform() { return m_LocalTransform; }
        public string GetBoneName() { return m_Name; }
        public int GetBoneID() { return m_ID; }

        /* Gets the current index on mKeyPositions to interpolate to based on
        the current animation time*/
        public int GetPositionIndex(float animationTime)
        {
            for (int index = 0; index < m_NumPositions - 1; ++index)
            {
                if (animationTime < m_Positions[index + 1].timeStamp)
                    return index;
            }
            //Debug.Assert(false);
            return 0;
        }

        /* Gets the current index on mKeyRotations to interpolate to based on the
        current animation time*/
        public int GetRotationIndex(float animationTime)
        {
            for (int index = 0; index < m_NumRotations - 1; ++index)
            {
                if (animationTime < m_Rotations[index + 1].timeStamp)
                    return index;
            }
            //Debug.Assert(false);
            return 0;
        }

        /* Gets the current index on mKeyScalings to interpolate to based on the
        current animation time */
        public int GetScaleIndex(float animationTime)
        {
            for (int index = 0; index < m_NumScalings - 1; ++index)
            {
                if (animationTime < m_Scales[index + 1].timeStamp)
                    return index;
            }
            //Debug.Assert(false);
            return 0;
        }


        /* Gets normalized value for Lerp & Slerp*/
        float GetScaleFactor(float lastTimeStamp, float nextTimeStamp, float animationTime)
        {
            float scaleFactor = 0.0f;
            float midWayLength = animationTime - lastTimeStamp;
            float framesDiff = nextTimeStamp - lastTimeStamp;
            scaleFactor = midWayLength / framesDiff;
            return scaleFactor;
        }

        Matrix4 InterpolatePosition(float animationTime)
        {
            if (1 == m_NumPositions)
                return Matrix4.CreateTranslation(m_Positions[0].position);

            int p0Index = GetPositionIndex(animationTime);
            int p1Index = p0Index + 1;
            float scaleFactor = GetScaleFactor(m_Positions[p0Index].timeStamp,
                m_Positions[p1Index].timeStamp, animationTime);

            float nx = m_Positions[p0Index].position.X + (m_Positions[p1Index].position.X - m_Positions[p0Index].position.X) * scaleFactor;
            float ny = m_Positions[p0Index].position.Y + (m_Positions[p1Index].position.Y - m_Positions[p0Index].position.Y) * scaleFactor;
            float nz = m_Positions[p0Index].position.Z + (m_Positions[p1Index].position.Z - m_Positions[p0Index].position.Z) * scaleFactor;
            Vector3 finalPosition = Vector3.Lerp(m_Positions[p0Index].position,
                m_Positions[p1Index].position, scaleFactor);
            return Matrix4.CreateTranslation(finalPosition);
        }

        /*figures out which rotations keys to interpolate b/w and performs the interpolation
        and returns the rotation matrix*/
        Matrix4 InterpolateRotation(float animationTime)
        {
            if (1 == m_NumRotations)
            {
                var rotation = m_Rotations[0].orientation.Normalized();
                return Matrix4.CreateFromQuaternion(rotation);
            }

            int p0Index = GetRotationIndex(animationTime);
            int p1Index = p0Index + 1;
            float scaleFactor = GetScaleFactor(m_Rotations[p0Index].timeStamp,
                m_Rotations[p1Index].timeStamp, animationTime);

            OpenTK.Mathematics.Quaternion q1, q2;

            q1 = m_Rotations[p0Index].orientation;
            q2 = m_Rotations[p1Index].orientation;

            
            OpenTK.Mathematics.Quaternion finalRotation = OpenTK.Mathematics.Quaternion.Slerp(m_Rotations[p0Index].orientation,
                m_Rotations[p1Index].orientation, scaleFactor);
            finalRotation = finalRotation.Normalized();
            return Matrix4.CreateFromQuaternion(finalRotation);
        }

        /*figures out which scaling keys to interpolate b/w and performs the interpolation
        and returns the scale matrix*/
       Matrix4 InterpolateScaling(float animationTime)
        {
            if (1 == m_NumScalings)
                return Matrix4.CreateScale(m_Scales[0].scale);

            int p0Index = GetScaleIndex(animationTime);
            int p1Index = p0Index + 1;
            float scaleFactor = GetScaleFactor(m_Scales[p0Index].timeStamp,
                m_Scales[p1Index].timeStamp, animationTime);
            Vector3 finalScale = Vector3.Lerp(m_Scales[p0Index].scale, m_Scales[p1Index].scale, scaleFactor);
            return Matrix4.CreateScale(finalScale);
        }

    }


}
