using UnityEngine;

public class Portal : Collidable
{

    public string[] sceneNames;

    // Start is called before the first frame update
    protected override void OnCollide(Collider2D coll)
    {

        if (coll.name == "Player")
        {
            // Teleport player to random dungeon 
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
