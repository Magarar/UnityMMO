using System;
using System.Collections;
using System.Collections.Generic;
using Character.Creatures;
using Items;
using Manager;
using Player;
using Server;
using TMPro;
using UIs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Character.Player
{
    public class PlayerManager : MonoBehaviour
    {
        //network boolean
        /// <summary>
        /// 0 = bot,
        /// 1 = player
        /// </summary>
        public bool IsLocalPlayer;
        public bool IsAttacking;
        public bool IsCasting;
        public bool IsCreature;
        public bool IsNpc;
        public bool IsCharacterDied;
        
        //respawn
        public bool IsRespawnLoading;
        public float maxLoadingScreenRespawn = 5f;
        public float minLoadingScreenRespawn = 1f;
            public string playerTarget {get; set; } = string.Empty;
        
        public Characters characters;
        public CharacterStat characterStat;
        
        public List<GameObject> raceList = new List<GameObject>();

        [Header("Components")]
        public Authentication authentication;
        public World world;
        public CreaturesDatabase creaturesDatabase;
        public PlayerNet playerNet;
        public PlayerVFX playerVFX;
        public PlayerCamera playerCameraScript;
        public CanvasHp canvasHp;
        public Slider HpBar;
        public TextMeshProUGUI setName;
        public TextMeshProUGUI setGuild;
        public CapsuleCollider myCollider;

        [HideInInspector]public Animation curAnimation;
        [HideInInspector]public Animator curAnimator;

        [Header("Locomotion")]
        public float moveSpeed;
        public float hitDistance;
        public float stopDistance;
        public float stopAttackDistance;

        private Ray r;
        public RaycastHit hit;
        public LayerMask hitMask;
        public NavMeshAgent agent;
        public Camera playerCamera;
        public Vector3 curPosition;
        
        [Header("Audio")]
        public List<AudioClip> playerSoundInterface = new List<AudioClip>();
        public AudioSource asInterface;

        private void Update()
        {
            HitToMove();
            CheckAnimation();
            UpdateHpBar();
            UpdateLoadingRespawn();
            UpdateAttackPlayer();
        }

        public void SpawnCreatureDamagePopUp(int dpsEvent,int totalDamage)
        {
            CanvasHp c = Instantiate(canvasHp, transform, false);
            c.gameObject.SetActive(true);
            c.hpBar.gameObject.SetActive(false);
            c.playerGuild.gameObject.SetActive(false);
            c.playerName.gameObject.SetActive(true);
            switch (dpsEvent)
            {
                case 0:
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.white;
                    c.playerName.text = totalDamage.ToString();
                    break;
                case 1://crit
                    c.playerName.fontSize = 2f;
                    c.playerName.color = new Color(255,160,0,255);
                    c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y + 0.5f, c.transform.position.z);
                    c.playerName.text = totalDamage.ToString();
                    break;
                case 2://dodge
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.black;
                    c.playerName.text = $"Dodge!";
                    break;
                case 3://miss
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.blue;
                    c.playerName.text = $"Miss!";
                    break;
                case 4:
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.gray;
                    c.playerName.text = $"Parry!";
                    break;
                case 5://dying
                    if (GameManager.Instance.characters_name == playerTarget)
                    {
                        c.playerName.fontSize = 1f;
                        c.playerName.color = Color.green;
                        c.playerName.text = $"EXP {totalDamage}!";
                    }
                    else
                    {
                        Destroy(c.gameObject);
                    }
                    canvasHp.gameObject.SetActive(false);
                    myCollider.enabled = false;
                    StartCoroutine(WaitAfterAttack(0.8f, () =>
                    {
                        PlayAnimation("Death");
                    }));
                    break;
                case 6:
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.green;
                    c.playerName.text = totalDamage.ToString();
                    playerVFX.OpenParticleSystem(2);
                    break;
                case 7:
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.blue;
                    c.playerName.text = totalDamage.ToString();
                    playerVFX.OpenParticleSystem(10);
                    break;
                case 8:
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.yellow;
                    c.playerName.text = totalDamage.ToString();
                    playerVFX.OpenParticleSystem(18);
                    break;
                    
            }
            c.isDamagePopUp = true;
        }

        public void SpawnPlayerDamagePopUp(int dpsEvent,int totalDamage)
        {
            CanvasHp c = Instantiate(canvasHp, transform, false);
            c.gameObject.SetActive(true);
            c.hpBar.gameObject.SetActive(false);
            c.playerGuild.gameObject.SetActive(false);
            c.playerName.gameObject.SetActive(true);
            switch (dpsEvent)
            {
                case 0:
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.white;
                    c.playerName.text = totalDamage.ToString();
                    break;
                case 1://crit
                    c.playerName.fontSize = 2f;
                    c.playerName.color = new Color(255,160,0,255);
                    c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y + 0.5f, c.transform.position.z);
                    c.playerName.text = totalDamage.ToString();
                    break;
                case 2://dodge
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.black;
                    c.playerName.text = $"Dodge!";
                    break;
                case 3://miss
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.blue;
                    c.playerName.text = $"Miss!";
                    break;
                case 4:
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.gray;
                    c.playerName.text = $"Parry!";
                    break;
                case 5://dying
                    PlayAnimation("Death");
                    GameManager.Instance.animationName = "Death";
                    if (GameManager.Instance.characters_name == gameObject.name)
                    {
                        GameManager.Instance.Target = null;
                        IsAttacking = false;
                        IsCasting = false;
                        IsCharacterDied = true;
                        RespawnMessageManager.Instance.RespawnSet($"You are dead!", () =>
                        {
                            world.TcpSendMessage($"RESPAWN PLAYER",null);
                        });

                    }
                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.red;
                    c.playerName.text = $"Died!";
                    canvasHp.gameObject.SetActive(false);
                    myCollider.enabled = false;
                   
                    break;
                case 6://respawn
                    //bot
                    if (!IsLocalPlayer)
                    {
                        
                    }
                    
                    
                    if (GameManager.Instance.characters_name == gameObject.name)
                    {
                        IsCharacterDied = false;
                        agent.isStopped = true;
                        PlayAnimation("IDLE");
                        GameManager.Instance.animationName = "IDLE";
                        IsRespawnLoading = true;
                        LoadingManager.Instance.ResetProgressBar();
                        Header.Instance.OpenGameUIHeader(5);
                        transform.position = CharactersManager.Instance.spawnCharacterPositionBase.position;
                        curPosition = Vector3.zero;
                    }
                    
                    canvasHp.gameObject.SetActive(true);
                    myCollider.enabled = true;
                    

                    c.playerName.fontSize = 1f;
                    c.playerName.color = Color.green;
                    c.playerName.text = $"Respawned!";
                    playerVFX.OpenParticleSystem(2);
                    break;
                    
            }
            c.isDamagePopUp = true;
        }

        public void OpenRace(int raceID)
        {
            CloseRace();
            raceList[raceID].SetActive(true);
        }

        public void CloseRace()
        {
            foreach (var race in raceList)
            {
                race.SetActive(false);
            }
        }

        private void OnMouseDown()
        {
            Debug.Log(gameObject.name);
        }
        
        
        /// <summary>
        /// true = hit on ui
        /// false = hit on object
        /// </summary>
        /// <returns></returns>
        private bool BlockRayCastForHitOnUI()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        private void HitToMove()
        {
            if(!IsLocalPlayer)
                return;
            if(GameManager.Instance.Is_Lock_Character)
                return;
            if(IsCharacterDied)
                return;
            r = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(r, out hit, hitDistance, hitMask))
            {
                if (BlockRayCastForHitOnUI())
                    return;

                if (hit.transform.CompareTag("Ground"))
                {
                    //drag
                    if (DragDropManager.Instance.isDragItem && !SplitManager.Instance.isDragItem)
                    {
                        world.TcpSendMessage($"INVENTORY THROW_DRAG {DragDropManager.Instance.curDragItem.slotID} {hit.point.x} {hit.point.y} {hit.point.z}",null);
                        DragDropManager.Instance.isDragItem = false;
                        DragDropManager.Instance.dragDropSlot.gameObject.SetActive(false);
                        return;
                    }
                    //split
                    if (!DragDropManager.Instance.isDragItem && SplitManager.Instance.isDragItem)
                    {
                        world.TcpSendMessage($"INVENTORY THROW_SPLIT {SplitManager.Instance.curDragItem.slotID} {SplitManager.Instance.splitItemCount} {hit.point.x} {hit.point.y} {hit.point.z}",null);
                        SplitManager.Instance.isDragItem = false;
                        SplitManager.Instance.dragDropSlot.gameObject.SetActive(false);
                        return;
                    }
                    return;
                }
                if (hit.transform.CompareTag("Item"))
                {
                    ItemPickUp ipu = hit.transform.GetComponent<ItemPickUp>();
                    world.TcpSendMessage($"INVENTORY ADD {ipu.netID} {ipu.itemID} {ipu.itemCount}",null);
                    return;
                }
            }
            
            if (Input.GetMouseButton(0)&&Physics.Raycast(r, out hit,hitDistance,hitMask))
            {
                if (BlockRayCastForHitOnUI())
                    return;
                if (hit.transform.CompareTag("Player"))
                {   
                    PlayerManager pm = hit.transform.GetComponent<PlayerManager>();
                    if(pm.IsLocalPlayer)
                        return;
                    if(!pm.IsCreature)
                        return;
                    if (!pm.IsLocalPlayer)
                    {
                        if (pm.creaturesDatabase.creatureType == CreatureType.Normal ||
                            pm.creaturesDatabase.creatureType == CreatureType.Friendly ||
                            pm.creaturesDatabase.creatureType == CreatureType.Rare ||
                            pm.creaturesDatabase.creatureType == CreatureType.Elite ||
                            pm.creaturesDatabase.creatureType == CreatureType.Boss)
                        {
                            IsCasting = false;
                            IsAttacking = true;
                            GameManager.Instance.Target = pm;
                            agent.stoppingDistance = stopAttackDistance;
                            PlayAnimation("MOVE");
                            GameManager.Instance.animationName = "MOVE";
                            return;
                        }
                        if (pm.creaturesDatabase.creatureType == CreatureType.Friend)
                            return;
                        
                    }
                    //Open any menu
                    PlayAnimation("IDLE");
                    GameManager.Instance.animationName = "IDLE";
                }

                if (hit.transform.CompareTag("Ground"))
                {
                    //disable player movement while chatting
                    
                    if(GameManager.Instance.IsChatting)
                        return;
                    
                    if(SplitManager.Instance.isArrow)
                        return;
                    IsCasting = false;
                    IsAttacking = false;
                    
                    agent.isStopped = false;
                    curPosition = hit.point;
                    PlayAnimation("MOVE");
                    GameManager.Instance.animationName = "MOVE";
                    agent.stoppingDistance = 0;
                }

            }

            if (!IsAttacking)
            {
                GameManager.Instance.attackTime = 0;
                if (curPosition != Vector3.zero)
                {
                    agent.SetDestination(curPosition);
                }

                if (Vector3.Distance(transform.position, curPosition) < stopDistance)
                {
                    PlayAnimation("IDLE");
                    GameManager.Instance.animationName = "IDLE";
                }
            }

            if (IsAttacking)
            {
                //follow
                if (GameManager.Instance.Target!=null && !IsCasting)
                {
                    agent.SetDestination(GameManager.Instance.Target.transform.position);
                }

                //check distance
                if (Vector3.Distance(transform.position, GameManager.Instance.Target.transform.position) < stopAttackDistance && !IsCasting)
                {
                    PlayAnimation("IDLE");
                    GameManager.Instance.animationName = "IDLE";
                    IsCasting = true;
                }
                
                //start attack
                if (IsCasting)
                {
                    //face
                    transform.LookAt(GameManager.Instance.Target.transform.position);
                    //check distance
                    if (Vector3.Distance(transform.position, GameManager.Instance.Target.transform.position)  > stopAttackDistance+0.1f)
                    {
                        PlayAnimation("IDLE");
                        GameManager.Instance.animationName = "IDLE";
                        IsCasting = false;
                        IsAttacking = false;
                        curPosition = Vector3.zero;
                    }

                    GameManager.Instance.attackTime -= Time.deltaTime;
                    if (GameManager.Instance.attackTime <= 0)
                    {
                        GameManager.Instance.attackTime = GameManager.Instance.characterStat_attack_time;
                        //attack
                        PlayAnimation("Attack");
                        GameManager.Instance.animationName = "Attack";
                        PlayInterface(0);
                        StartCoroutine(WaitAfterAttack(1f, () =>
                        {
                            if (!IsCharacterDied)
                            {
                                PlayAnimation("IDLE");
                                GameManager.Instance.animationName = "IDLE";
                            }
                            
                        }));
                        //send tcp packets
                        world.TcpSendMessage($"ATTACK CREATURE {GameManager.Instance.Target.creaturesDatabase.creature_guid}",null);
                        //check if enemy is die
                    }

                    if (GameManager.Instance.Target.HpBar.value <= 0)
                    {
                        EnemyIsDying();
                    }
                }
            }

            
        }

        private IEnumerator WaitAfterAttack(float t, Action c)
        {
            yield return new WaitForSeconds(t);
            c?.Invoke();
        }

        public void EnemyIsDying()
        {
            if (GameManager.Instance.Target.HpBar.value <= 0)
            {
                IsCasting = false;
                IsAttacking = false;
                PlayAnimation("IDLE");
                GameManager.Instance.animationName = "IDLE";
                agent.stoppingDistance = 0;
                curPosition = transform.position;
                            
                GameManager.Instance.Target = null;
                return;
            }
        }

        public void CheckAnimation()
        {
            foreach (var c in raceList)
            {
                if (c.activeInHierarchy)
                {
                    if (c.TryGetComponent<Animation>(out var a))
                    {
                        curAnimation = a;
                        break;
                    }
        
                    if (c.TryGetComponent<Animator>(out var b))
                    {
                        curAnimator = b;
                        break;
                    }
                }
            }
        }

        private void UpdateHpBar()
        {
            canvasHp.transform.eulerAngles = playerCamera.transform.eulerAngles;
        }
        
        private void UpdateLoadingRespawn()
        {
            if (IsRespawnLoading)
            {
                LoadingManager.Instance.SetLoadingProgressMax((int)maxLoadingScreenRespawn);
                minLoadingScreenRespawn -= Time.deltaTime;
                if (minLoadingScreenRespawn <= 0)
                {
                    minLoadingScreenRespawn =1;
                    LoadingManager.Instance.SetLoadingProgressMin(1);
                    if (LoadingManager.Instance.isProgressBarFull)
                    {
                        IsRespawnLoading = false;
                        Header.Instance.OpenGameUIHeader(6);
 
                    }
                }
            }
        }

        public void PlayAnimation(string animationName)
        {
            if (curAnimation != null)
                curAnimation.Play(animationName);
            if (curAnimator != null)
                curAnimator.Play(animationName);
            if(IsLocalPlayer)
                Debug.Log($"PlayAnimation {animationName}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsLocalPlayer && other.gameObject.CompareTag("City"))
            {
                GameManager.Instance.Is_Inside_City = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsLocalPlayer && other.gameObject.CompareTag("City"))
            {
                GameManager.Instance.Is_Inside_City = false;
            }
        }

        public void PlayInterface(int interfaceIndex)
        {
            asInterface.clip = playerSoundInterface[interfaceIndex];
            asInterface.Play();
        }

        
        //use for bot only
        private void UpdateAttackPlayer()
        {
            if(!GameManager.Instance.Is_Start_Game)
                return;
            if(!IsCreature)
                return;
            if(HpBar.value <= 0)
                return;

            if (playerTarget == string.Empty)
            {
                float x = float.Parse(creaturesDatabase.creature_positionX);
                float y = float.Parse(creaturesDatabase.creature_positionY);
                float z = float.Parse(creaturesDatabase.creature_positionZ);
                var myPosition = new Vector3(x,y,z);
                if (transform.position == myPosition)
                {
                    PlayAnimation("IDLE");
                    return;
                }
                IsAttacking = false;

                agent.SetDestination(myPosition);
                agent.stoppingDistance = 0;
            }
            else
            {
                if (CharactersManager.Instance.spawnPlayerDict.TryGetValue(playerTarget, out var _player))
                {
                    agent.stoppingDistance = 3;
                    float distance = Vector3.Distance(transform.position, _player.transform.position);
                    if (distance <= agent.stoppingDistance)
                    {
                        if (!IsAttacking)
                        {
                            PlayAnimation("IDLE");
                            IsAttacking = true;
                            creaturesDatabase.creature_attackTime_min = 0.5f;
                        }
                        else
                        {
                            CreaturesDatabase cd = creaturesDatabase;
                            cd.creature_attackTime_min -= Time.deltaTime;
                            if (cd.creature_attackTime_min <= 0)
                            {
                                cd.creature_attackTime_min = cd.creature_attackTime_max;
                                PlayAnimation("Attack");
                                
                                //sync udp packet
                                if (GameManager.Instance.characters_name == playerTarget)
                                {
                                    playerNet.UdpSendMessage($"SYNC CREATURE_ATTACK {GameManager.Instance.realmlist_guid} {creaturesDatabase.creature_guid} {playerTarget}",GameManager.Instance.realmlist_ipaddress, int.Parse(GameManager.Instance.realmlist_port));
                                }
                                
                                StartCoroutine(WaitAfterAttack(1f, () =>
                                {
                                    if (HpBar.value > 0)
                                    {
                                        PlayAnimation("IDLE");
                                    }
                                }));
                            }
                        }
                    }
                    else
                    {
                        IsAttacking = false;
                        agent.SetDestination(_player.transform.position);
                        agent.transform.LookAt(_player.transform.position);
                        PlayAnimation("WALK");
                    }
                }
            }
            
            
        }
    }
}
