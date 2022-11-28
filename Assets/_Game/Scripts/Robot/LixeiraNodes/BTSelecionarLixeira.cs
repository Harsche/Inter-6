using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSelecionarLixeira : BTNode
{
    private GameObject player = Player.Instance.gameObject;

    private bool verificando;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        if (Vector3.Distance(player.transform.position, bt.transform.position) > 5f)
        {
            verificando = true;
        }

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 5f)
        {
            verificando = false;
        }

        while(verificando)
        {
            if (bt.npcRef.lixeiraIndex >= bt.npcRef.lixeiras.Count - 1f)
            {
                bt.npcRef.lixeiraIndex = 0;
            }
            else if (bt.npcRef.lixeiraIndex <= bt.npcRef.lixeiras.Count && bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu == false)
            {
                bt.npcRef.lixeiraIndex++;
            }

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}
