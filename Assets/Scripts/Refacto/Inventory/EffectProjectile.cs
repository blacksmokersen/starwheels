using UnityEngine;

[CreateAssetMenu]
public class EffectProjectile : Effect
{
    public GameObject ProjectilePrefab;
    public ProjectileLaunchType LaunchType;

    [Header("LaunchType: Straight")]
    public float Speed;

    [Header("LaunchType: Arc")]
    public float ForwardThrowingForce;
    public float TimesLongerThanHighThrow;

    public enum ProjectileLaunchType { Straight, Arc }

    // PUBLIC
    public override void Execute(GameObject source)
    {
        SpawnProjectile(source);
    }

    // PRIVATE
    private void SpawnProjectile(GameObject source)
    {
        ProjectileLauncher launcher = source.GetComponent<ProjectileLauncher>();
        // TODO: Photon Instantiate
        GameObject projectile = Instantiate(ProjectilePrefab);
        Transform transform = projectile.transform;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 rot = Vector3.zero;

        if (launcher.Direction == Direction.Forward || launcher.Direction == Direction.Default)
        {
            transform.position = launcher.FrontPosition.position;
            rot = new Vector3(0, source.transform.rotation.eulerAngles.y, 0);

            if (LaunchType == ProjectileLaunchType.Straight)
            {
                rb.velocity = source.transform.TransformDirection(new Vector3(0, 0, 1)).normalized * Speed;
            }
            else if (LaunchType == ProjectileLaunchType.Arc)
            {
                var aimVector = source.transform.forward;
                rb.AddForce((aimVector + source.transform.up / TimesLongerThanHighThrow) * ForwardThrowingForce, ForceMode.Impulse);
            }
        }
        else if (launcher.Direction == Direction.Backward)
        {
            transform.position = launcher.BackPosition.position;
            rot = new Vector3(0, source.transform.rotation.eulerAngles.y + 180, 0);

            if (LaunchType == ProjectileLaunchType.Straight)
            {
                rb.velocity = -source.transform.forward * Speed;
            }
        }

        transform.rotation = Quaternion.Euler(rot);
    }
}
