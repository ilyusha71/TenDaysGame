using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimationGeneratorEditor : EditorWindow
{
    Texture m_texture;
    List<Sprite> sprites;
    private Vector2 scrollPos;
    AnimatorController ac;
    AnimationClip[] animClip = new AnimationClip[5];

    [MenuItem("Animation/AnimationGenerator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AnimationGeneratorEditor));
    }
    static void Initialize()
    {
        AnimationGeneratorEditor window = (AnimationGeneratorEditor)EditorWindow.GetWindow(
            typeof(AnimationGeneratorEditor),
            true,
            "Animation Generating Tools"
        );
        window.position = new Rect(0, 0, 250, 150);
    }
    public void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        // this.editor.OnInspectorGUI();
        Draw();
        EditorGUILayout.EndScrollView();
    }
    void Draw()
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            m_texture = EditorGUILayout.ObjectField(m_texture, typeof(Texture), true, GUILayout.Height(100), GUILayout.Width(100)) as Texture;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            if(m_texture)
            GUILayout.Label(m_texture.name, GUILayout.Width(100));
            GUILayout.EndVertical();
            GUILayout.Space(16);
            GUILayout.BeginVertical();
            {
                ac = EditorGUILayout.ObjectField(ac, typeof(AnimatorController), true, GUILayout.Height(30), GUILayout.Width(200)) as AnimatorController;
                GUILayout.Space(12);
                if (!animClip[0]) animClip[0] = AssetDatabase.LoadAssetAtPath("assets/Doraemon/Animations/Idle.anim", typeof(AnimationClip)) as AnimationClip;
                animClip[0] = EditorGUILayout.ObjectField(animClip[0], typeof(AnimationClip), true, GUILayout.Height(30), GUILayout.Width(200)) as AnimationClip;
                GUILayout.Space(12);
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Generate", GUILayout.Height(25), GUILayout.Width(66)))
                    {
                        animClip[1] = Generate(0);
                        AssetDatabase.CreateAsset(animClip[1], "assets/Doraemon/Animations/" + m_texture.name + "_Down.anim");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        animClip[2] = Generate(1);
                        AssetDatabase.CreateAsset(animClip[2], "assets/Doraemon/Animations/" + m_texture.name + "_Left.anim");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        animClip[3] = Generate(2);
                        AssetDatabase.CreateAsset(animClip[3], "assets/Doraemon/Animations/" + m_texture.name + "_Right.anim");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        animClip[4] = Generate(3);
                        AssetDatabase.CreateAsset(animClip[4], "assets/Doraemon/Animations/" + m_texture.name + "_Up.anim");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                    if (GUILayout.Button("Match", GUILayout.Height(25), GUILayout.Width(66)))
                    {
                        AnimatorStateMachine stateMachine = ac.layers[0].stateMachine;
                        AnimatorState stateWithBlendTree = stateMachine.states[0].state;
                        BlendTree blendTree = (BlendTree)stateWithBlendTree.motion;
                        while (blendTree.children.Length > 0)
                        { blendTree.RemoveChild(0); }
                        blendTree.AddChild(animClip[0], new Vector2(0, 0));
                        blendTree.AddChild(animClip[1], new Vector2(0, -0.1f));
                        blendTree.AddChild(animClip[2], new Vector2(-0.1f, 0));
                        blendTree.AddChild(animClip[3], new Vector2(0.1f, 0));
                        blendTree.AddChild(animClip[4], new Vector2(0, 0.1f));
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(16);

        sprites = new List<Sprite>();
        Object[] data = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(m_texture));
        if (data != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            //    data[0] =
            //Debug.Log(data.Length);//17
            foreach (Object obj in data)
            {
                if (obj.GetType() == typeof(Sprite))
                {
                    GUILayout.BeginVertical();
                    sprites.Add(EditorGUILayout.ObjectField(obj, typeof(Sprite), true, GUILayout.Height(100), GUILayout.Width(100)) as Sprite);
                    GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                    GUILayout.Label(obj.name, GUILayout.Width(100));
                    GUILayout.EndVertical();
                    if (sprites.Count % 4 == 0)
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                    }
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(16);

       
        GUILayout.Space(16);
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;    
    }
    public AnimationClip Generate(int queue)
    {
        AnimationClip animClip = new AnimationClip();
        AnimationClipSettings myClipSettings = AnimationUtility.GetAnimationClipSettings(animClip);
        myClipSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(animClip, myClipSettings);
        animClip.frameRate = 12;   // FPS
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";
        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[4];
        for (int i = 0; i < 4; i++)
        {
            spriteKeyFrames[i] = new ObjectReferenceKeyframe();
            spriteKeyFrames[i].time = ((float)i / 12);
            spriteKeyFrames[i].value = sprites[i+queue*4];
        }
        AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);
        return animClip;
    }
}