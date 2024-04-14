using UnityEngine;

namespace CharacterEditor
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private MeshRenderer m_Head;
        [SerializeField] private MeshRenderer m_Body;
        [SerializeField] private MeshRenderer m_ArmR;
        [SerializeField] private MeshRenderer m_ArmL;
        [SerializeField] private MeshRenderer m_LegR;
        [SerializeField] private MeshRenderer m_LegL;

        private void Start()
        {
            Load();
        }

        public void Load()
        {
            //Load materials from the MaterialManager and pass in the id pulled from each PlayerPref here

            int headMaterialID = PlayerPrefs.GetInt("HeadMaterialID", 0);
            int bodyMaterialID = PlayerPrefs.GetInt("BodyMaterialID", 0);
            int legsMaterialID = PlayerPrefs.GetInt("LegsMaterialID", 0);

            Material headMaterial = MaterialManager.Get(BodyTypes.Head, headMaterialID);
            Material bodyMaterial = MaterialManager.Get(BodyTypes.Body, bodyMaterialID);
            Material legsMaterial = MaterialManager.Get(BodyTypes.Leg, legsMaterialID);

            // Assign loaded materials to each body part
            if (m_Head != null)
                m_Head.material = headMaterial;

            if (m_Body != null)
                m_Body.material = bodyMaterial;

            if (m_ArmR != null)
                m_ArmR.material = bodyMaterial; 

            if (m_ArmL != null)
                m_ArmL.material = bodyMaterial; 

            if (m_LegR != null)
                m_LegR.material = legsMaterial;

            if (m_LegL != null)
                m_LegL.material = legsMaterial;
        }
    }
}