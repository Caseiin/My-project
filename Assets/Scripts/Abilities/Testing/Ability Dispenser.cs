    using System.Collections.Generic;
    using UnityEngine;

    public class AbilityDispenser : MonoBehaviour
    {
        [SerializeField] List<AbilitySO> _abilityList;
        [SerializeField] GameObject _abilityObject;
        [SerializeField] InputReader _input;
        [SerializeField] float minSpawnRadius = 2f;
        [SerializeField] float maxSpawnRadius = 5f;

        GameObject AbilityFactory(AbilitySO ability)
        {
            var abilityInstance = Instantiate(_abilityObject);
            var tester = abilityInstance.GetComponent<Tester>();
            var mesh = abilityInstance.GetComponent<MeshRenderer>();
            mesh.material = ability.abilityMaterial;
            tester.Ability = ability;
            return abilityInstance;
        }

        void PlaceAbilityTrip(GameObject ability)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            float distance = Random.Range(minSpawnRadius, maxSpawnRadius);

            Vector3 spawnOffset = new Vector3(direction.x, 0, direction.y) * distance;
            Vector3 spawnPosition = transform.position + spawnOffset;

            ability.transform.position = spawnPosition;
        }

        void DispenseAbility()
        {
            if (_abilityList.Count == 0) return;
            var ability = _abilityList[Random.Range(0, _abilityList.Count)];
            var abilityObject = AbilityFactory(ability);
            PlaceAbilityTrip(abilityObject);
        }

        void OnEnable()
        {
            _input.OnShootTriggered += DispenseAbility;
        }

        void OnDisable()
        {
            _input.OnShootTriggered -= DispenseAbility;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxSpawnRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minSpawnRadius);
        }
    }
