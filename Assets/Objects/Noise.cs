using UnityEngine;
//dodane przez kube - aktualnie nieuzywane
class Noise
{
    private float mean;
    private float diff;

    public Noise(float mean)
    {
        this.mean = mean;
        this.diff = 0;
    }

    public void change(float alpha, float beta)
    {
        diff = diff * alpha + Mathf.Sin(3 * Time.time) * beta;
    }
    
    public float get()
    {
        return mean + diff *Time.deltaTime;
    }
}
