using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class TestWindow : EditorWindow
{
    public static int _lableScale = 15;
    public static float _lableSpace = 20;
    private static float _scale = 1, _defaultScale = 1;
    private static float _minScale = 0.1f;
    private static float _maxScale = 1f;
    public static Color _eyes_Color;
    public static Color _bodyA_Color;
    public static Color _bodyB_Color;
    public static Color _commonA_Color;
    public static Color _commonB_Color;
    public static Material _eyes_Material;
    public static Material _bodyA_Material;
    public static Material _bodyB_Material;
    public static Material _commonA_Material;
    public static Material _commonB_Material;
    private GUIStyle _defaultStyle;
    private GUIStyle _grayStyle_1;
    private GUIStyle _grayStyle_2;
    private GUIStyle _materialsStyle;
    private GUIStyle _scaleStyle;
    private GUIStyle _alignmentStyle;
    private GUIStyle _scenesStyle;


    [MenuItem("Custom Editor/Test Window %#e")]
    private static void ShowWindow()
    {
        var window = GetWindow<TestWindow>();
        window.titleContent = new GUIContent("Test Window");
        window.Show();
    }

    private void OnEnable()
    {
        // Default. 
        _defaultStyle = new GUIStyle();
        _defaultStyle.fontSize = 12;
        _defaultStyle.normal.textColor = Color.white;
        _defaultStyle.richText = false;
        
        // Gray 1. 
        _grayStyle_1 = new GUIStyle();
        _grayStyle_1.fontSize = 12;
        _grayStyle_1.normal.textColor = Color.red * 0.85f;
        _grayStyle_1.richText = false;
        _grayStyle_1.alignment = TextAnchor.MiddleCenter;
        
        // Gray 2.
        _grayStyle_2 = new GUIStyle();
        _grayStyle_2.fontSize = _grayStyle_1.fontSize;
        _grayStyle_2.normal.textColor = _grayStyle_1.normal.textColor;
        _grayStyle_2.richText = _grayStyle_1.richText;
        _grayStyle_2.alignment = _grayStyle_1.alignment;

        // Material.
        _materialsStyle = new GUIStyle();
        _materialsStyle.fontSize = _lableScale;
        _materialsStyle.normal.textColor = Color.cyan;
        _materialsStyle.richText = true;
        _materialsStyle.alignment = TextAnchor.MiddleCenter;

        // Scale.
        _scaleStyle = new GUIStyle();
        _scaleStyle.fontSize = _lableScale;
        _scaleStyle.normal.textColor = Color.yellow;
        _scaleStyle.richText = true;
        _scaleStyle.alignment = TextAnchor.MiddleCenter;

        // Alignment.
        _alignmentStyle = new GUIStyle();
        _alignmentStyle.fontSize = _lableScale;
        _alignmentStyle.normal.textColor = Color.green;
        _alignmentStyle.richText = true;
        _alignmentStyle.alignment = TextAnchor.MiddleCenter;

        // Scenes.
        _scenesStyle = new GUIStyle();
        _scenesStyle.fontSize = _lableScale;
        _scenesStyle.normal.textColor = Color.magenta;
        _scenesStyle.richText = true;
        _scenesStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void OnGUI()
    {
        // Materials.
        GUILayout.Space(_lableSpace);
        EditorGUILayout.BeginVertical("box");
            GUILayout.Label("<b>Materials</b>", _materialsStyle);
            GUILayout.Label("  Dog materials", _defaultStyle);
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
                GUILayout.Label("Eyes", _grayStyle_1);
                GUILayout.Label("Body A", _grayStyle_1);
                GUILayout.Label("Body B", _grayStyle_1);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
                _eyes_Material = EditorGUILayout.ObjectField(_eyes_Material, typeof(Material), true) as Material;
                _bodyA_Material = EditorGUILayout.ObjectField(_bodyA_Material, typeof(Material), true) as Material;
                _bodyB_Material = EditorGUILayout.ObjectField(_bodyB_Material, typeof(Material), true) as Material;
            GUILayout.EndHorizontal();
            if (!_eyes_Material || !_bodyA_Material || !_bodyB_Material)
            {
                EditorGUILayout.HelpBox("Assign materials", MessageType.Error);
                _grayStyle_1.normal.textColor = Color.red * 0.85f;
            }
            else
            {
                GUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    _grayStyle_1.normal.textColor = Color.gray;
                    Undo.RecordObject(_eyes_Material, "ChangeColor");
                    _eyes_Color = _eyes_Material.color;
                    _eyes_Color = EditorGUILayout.ColorField(_eyes_Color);
                    if (EditorGUI.EndChangeCheck())
                    {
                        SetColor(_eyes_Material, _eyes_Color);
                    }
                    EditorGUI.BeginChangeCheck();
                    Undo.RecordObject(_bodyA_Material, "ChangeColor");
                    _bodyA_Color = _bodyA_Material.color;
                    _bodyA_Color = EditorGUILayout.ColorField(_bodyA_Color);
                    if (EditorGUI.EndChangeCheck())
                    {
                        SetColor(_bodyA_Material, _bodyA_Color);
                    }
                    EditorGUI.BeginChangeCheck();
                    Undo.RecordObject(_bodyB_Material, "ChangeColor");
                    _bodyB_Color = _bodyB_Material.color;
                    _bodyB_Color = EditorGUILayout.ColorField(_bodyB_Color);
                    if (EditorGUI.EndChangeCheck())
                    {
                        SetColor(_bodyB_Material, _bodyB_Color);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Random", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
                    {
                        Undo.RecordObject(_eyes_Material, "ChangeColor");
                        SetRandomColor(_eyes_Material, ref _eyes_Color);
                    }
                    if (GUILayout.Button("Random", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
                    {
                        Undo.RecordObject(_bodyA_Material, "ChangeColor");
                        SetRandomColor(_bodyA_Material, ref _bodyA_Color);
                    }
                    if (GUILayout.Button("Random", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
                    {
                        Undo.RecordObject(_bodyB_Material, "ChangeColor");
                        SetRandomColor(_bodyB_Material, ref _bodyB_Color);
                    }
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(_lableSpace);
            GUILayout.Label("  Common materials", _defaultStyle);
            EditorGUILayout.Separator();
            GUILayout.BeginHorizontal();
                GUILayout.Label("Common A", _grayStyle_2);
                GUILayout.Label("Common B", _grayStyle_2);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
                _commonA_Material = EditorGUILayout.ObjectField(_commonA_Material, typeof(Material), true) as Material;
                _commonB_Material = EditorGUILayout.ObjectField(_commonB_Material, typeof(Material), true) as Material;
            GUILayout.EndHorizontal();
            if (!_commonA_Material || !_commonB_Material)
            {
                EditorGUILayout.HelpBox("Assign materials", MessageType.Error);
                _grayStyle_2.normal.textColor = Color.red * 0.85f;
            }
            else
            {
                GUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    _grayStyle_2.normal.textColor = Color.gray;
                    Undo.RecordObject(_commonA_Material, "ChangeColor");
                    _commonA_Color = _commonA_Material.color;
                    _commonA_Color = EditorGUILayout.ColorField(_commonA_Color);
                    if (EditorGUI.EndChangeCheck())
                    {
                        SetColor(_commonA_Material, _commonA_Color);
                    }
                    EditorGUI.BeginChangeCheck();
                    Undo.RecordObject(_commonB_Material, "ChangeColor");
                    _commonB_Color = _commonB_Material.color;
                    _commonB_Color = EditorGUILayout.ColorField(_commonB_Color);
                    if (EditorGUI.EndChangeCheck())
                    {
                        SetColor(_commonB_Material, _commonB_Color);
                    }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Random", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
                    {
                        Undo.RecordObject(_commonA_Material, "ChangeColor");
                        SetRandomColor(_commonA_Material, ref _commonA_Color);
                    }
                    if (GUILayout.Button("Random", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
                    {
                        Undo.RecordObject(_commonB_Material, "ChangeColor");
                        SetRandomColor(_commonB_Material, ref _commonB_Color);
                    }
                GUILayout.EndHorizontal();

                EditorGUILayout.Separator();
                GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Set Common A", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
                    {
                        SetMaterial(_commonA_Material);
                    }
                    if (GUILayout.Button("Set Common B", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
                    {
                        SetMaterial(_commonB_Material);
                    }
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Set Random Material", GUILayout.MinWidth(200), GUILayout.MinHeight(50)))
                {
                    SetRandomMaterial();
                }
            }
        EditorGUILayout.EndVertical();

        // Scale.
        GUILayout.Space(_lableSpace);
        EditorGUILayout.BeginVertical("box");
            GUILayout.Label("<b>Scale</b>", _scaleStyle);
            EditorGUI.BeginChangeCheck();
            _scale = EditorGUILayout.Slider(_scale, 0.1f, 1f);
            if (EditorGUI.EndChangeCheck())
            {
                SetScale(_scale);
            }
            GUILayout.Label($"Random range scale: from ({_minScale}) to ({_maxScale})");
            EditorGUILayout.MinMaxSlider(ref _minScale, ref _maxScale, 0.1f, 1f);
            if (GUILayout.Button("Random Scale", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
            {
                SetRandomScale();
            }
            if (GUILayout.Button("Default Scale", GUILayout.MinWidth(100), GUILayout.MinHeight(30)))
            {
                SetDefaultScale();
            }
        EditorGUILayout.EndVertical();

        // Alignment.
        GUILayout.Space(_lableSpace);
        EditorGUILayout.BeginVertical("box");
            GUILayout.Label("<b>Alignment</b>", _alignmentStyle);
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                    if (GUILayout.Button("Scatter", GUILayout.MinHeight(30)))
                    {
                        Scatter();
                    }
                    if (GUILayout.Button("Align", GUILayout.MinHeight(30)))
                    {
                        Align();
                    }
                EditorGUILayout.EndVertical();
                if (GUILayout.Button("Default Position", GUILayout.MinHeight(62)))
                {
                    SetDefaultPosition();
                }
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        // Scenes
        GUILayout.Space(_lableSpace);
        EditorGUILayout.BeginVertical("box");
            GUILayout.Label("<b>Scenes</b>", _scenesStyle);
            for (int i = 0; i < EditorSceneManager.sceneCountInBuildSettings; i++)
            {
                string name = SceneUtility.GetScenePathByBuildIndex(i);
                if (GUILayout.Button(name, GUILayout.MinHeight(30)))
                {
                    EditorSceneManager.SaveOpenScenes();
                    EditorSceneManager.OpenScene(name);
                }
            }
        EditorGUILayout.EndVertical();
    }

    private void SetColor(Material material, Color color)
    {
        material.color = color;

    }
    private void SetRandomColor(Material material, ref Color color)
    {
        Color randomColor = Random.ColorHSV();
        material.color = randomColor;
        color = randomColor;
    }

    private void SetRandomMaterial()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            if (Selection.gameObjects[i].tag != "Untagged") continue;

            Undo.RecordObject(Selection.gameObjects[i].GetComponent<Renderer>(), "SetRandomMaterial");
            if (Random.Range(0, 2) == 0)
            {
                Selection.gameObjects[i].GetComponent<Renderer>().material = _commonA_Material;
            }
            else
            {
                Selection.gameObjects[i].GetComponent<Renderer>().material = _commonB_Material;
            }
        }
    }

    private void SetMaterial(Material material)
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            if (Selection.gameObjects[i].tag != "Untagged") continue;

            Undo.RecordObject(Selection.gameObjects[i].GetComponent<Renderer>(), "SetMaterial");
            Selection.gameObjects[i].GetComponent<Renderer>().material = material;
        }
    }

    private void SetScale(float scale)
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "SetScale");
            Selection.gameObjects[i].transform.localScale = Vector3.one * scale;
        }
    }

    private void SetRandomScale()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "SetScale");
            _scale = Random.Range(_minScale, _maxScale);
            Selection.gameObjects[i].transform.localScale = Vector3.one * _scale; ;
        }
    }

    private void SetDefaultScale()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "SetScale");
            Selection.gameObjects[i].transform.localScale = Vector3.one * _defaultScale;
            _scale = _defaultScale;
        }
    }

    private void Scatter()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            float min = -0.5f;
            float max = 0.5f;
            Undo.RecordObject(Selection.gameObjects[i].transform, "Scatter");
            Vector3 randomVector = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
            Selection.gameObjects[i].transform.position += randomVector;
        }
    }

    private void Align()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "Align");
            Selection.gameObjects[i].name = $"AlignedCube {i}";

            Undo.RecordObject(Selection.gameObjects[i].transform, "Align");
            Vector3 position = Selection.gameObjects[i].transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            position.z = Mathf.Round(position.z);
            Selection.gameObjects[i].transform.position = position;
        }
    }

    private void SetDefaultPosition()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            Undo.RecordObject(Selection.gameObjects[i].transform, "SetDefaultPosition");
            Vector3 position = Selection.gameObjects[i].GetComponent<Cube>().DefaultPosition.Value;
            Selection.gameObjects[i].transform.position = position;
        }
    }
}
