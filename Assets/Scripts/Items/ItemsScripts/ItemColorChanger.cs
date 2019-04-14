using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Multiplayer;
using Multiplayer.Teams;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace Items
{
    public class ItemColorChanger : EntityBehaviour<IItemState>
    {
        [Header("Materials and Mesh renderers")]
        [SerializeField] private List<MeshRenderer> _meshRenderers;
        [SerializeField] private List<DecalProjectorComponent> _decals;

        private TeamColorSettings _colorSettings;

        // CORE

        private void Awake()
        {
            _colorSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings).ColorSettings;
        }

        // BOLT

        public override void Attached()
        {
            ChangeColorsUsingSettings(_colorSettings);
        }

        // PUBLIC

        public void ChangeColorsUsingSettings(TeamColorSettings settings)
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material.SetColor("_BaseColor", _colorSettings.ItemsColor);
            }

            foreach (var decal in _decals)
            {
                /*
                //Material material = new Shader(Shader.Find("HDRenderPipelin/Unlit"));
                var newMat = new Material(decal.m_Material);
                decal.m_Material = newMat;
                //decal.m_Material.color = _colorSettings.ItemsColor;
                decal.m_Material.SetColor("_BaseColor",_colorSettings.ItemsColor);
                */
            }
        }
    }
}
