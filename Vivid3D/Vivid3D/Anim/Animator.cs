using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene;
using Vivid.Maths;
using OpenTK.Mathematics;

namespace Vivid.Anim
{
  
    public class Animator
    {
        public SkeletalEntity Entity = null;
    public Animator() { }
        public Animator(Animation animation)
        {
            m_CurrentTime = 0.0f;
            m_CurrentAnimation = animation;

            m_FinalBoneMatrices = new Matrix4[100];

            for (int i = 0; i < 100; i++)
                m_FinalBoneMatrices[i] = Matrix4.Identity;
        }

        public void SetTime(float t)
        {
            m_CurrentTime = t;
            CalculateBoneTransform(m_CurrentAnimation.GetRootNode(), Matrix4.Identity);
        }

        public void UpdateAnimation(float dt)
        {
            m_DeltaTime = dt;
            if (m_CurrentAnimation != null)
            {
                m_CurrentTime += m_CurrentAnimation.GetTicksPerSecond() * dt;
                m_CurrentTime = m_CurrentTime % m_CurrentAnimation.GetDuration();
                if (m_CurrentTime >= (m_CurrentAnimation.GetDuration() - 1.0f))
                {

                }
                else
                {
                    CalculateBoneTransform(m_CurrentAnimation.GetRootNode(), Matrix4.Identity);
                }
            }
        }

        public void PlayAnimation(Animation animation)
        {
            m_CurrentAnimation = animation;
            m_CurrentTime = 0.0f;
        }

        public void CalculateBoneTransform(AssimpNodeData node, Matrix4 parentTransform)
        {
            string nodeName = node.name;
            Matrix4 nodeTransform = node.transformation;

            Bone bone = m_CurrentAnimation.FindBone(nodeName);

            if (bone != null)
            {
                bone.Update(m_CurrentTime);
                nodeTransform = bone.GetLocalTransform();
            }

             Matrix4 globalTransformation = nodeTransform*parentTransform;
          //  Matrix4 globalTransformation = parentTransform * nodeTransform;

            var boneInfoMap = m_CurrentAnimation.GetBoneIDMap();
            if (boneInfoMap.ContainsKey(nodeName))
            {
                int index = boneInfoMap[nodeName].id;
                Matrix4 offset = boneInfoMap[nodeName].offset;
                m_FinalBoneMatrices[index] = offset * globalTransformation;

                    //m_FinalBoneMatrices[index] = globalTransformation * offset;

            }

            for (int i = 0; i < node.children.Count; i++)
                CalculateBoneTransform(node.children[i], globalTransformation);
        }

        public Matrix4[] GetFinalBoneMatrices()
        {
            return m_FinalBoneMatrices;
        }

        public Animation GetAnimation()
        {
            return m_CurrentAnimation;
        }

        public void ResetBones()
        {
            for (int i = 0; i < m_FinalBoneMatrices.Length; i++)
                m_FinalBoneMatrices[i] = Matrix4.Identity;
        }

    
        Matrix4[] m_FinalBoneMatrices;
        Animation m_CurrentAnimation = null;
        float m_CurrentTime = 0.0f;
        float m_DeltaTime = 0.0f;
    };
}
