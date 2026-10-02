# 세로 깃발 및 VR 문 안내

2026-10-02. 적용 씬: `Assets/Scenes/main_playoursound.unity`.

- 기존 활성 경로 깃발 11개: 체험장 9개, 사방치기 2개. `Experience.png`, `Sabangchigi.png`와 `Guide_*.mat`를 사용한다.
- 문 앞 새 깃발: 장구·사방치기 각 1개를 기존 목재 안내판 옆에 배치했다. 두 깃발의 등불과 연결 팔은 제거했으며 안내면은 마을 입구에서 접근하는 길을 향한다. 따뜻한 미색 바탕과 청록·주홍·황토 장식, 이순신 Bold 세로 글자. `Warm_Janggu.png`, `Warm_Sabangchigi.png` 및 해당 재질을 사용한다. 리본은 글자를 가리지 않도록 천 바깥으로 옮겼다. 복제본 Collider는 비활성이다. 다른 경로 깃발의 등불은 유지했다.
- `Entrance_Guide_DoubleSided.asset`: 원본 제비꼬리 메시 복제. 위치·노멀·삼각형을 유지하고 +Z 면 UV의 U만 `1-U`로 보정했다. 원본 메시와 공유 천 재질은 변경하지 않았다.
- 메인 씬의 두 문 UI는 기존 목재 안내판으로 복원했다. 원본 글꼴·색상·텍스트 위치를 사용하며 문구·문 이동 로직·참조는 유지한다. 반투명 패널은 메인 씬에서 제거했다.
- 반투명 시안은 `../Prefabs/JangguFloatingDoorPanel.prefab`, `../Prefabs/SabangchigiFloatingDoorPanel.prefab`으로 저장했다. 목재 면·기둥을 제외한 독립 프리팹이다. `DoorPanel_Glass`, `DoorPanel_TextBackdrop`, `DoorPanel_Trim`의 URP/Lit 재질 연결과 제목·본문 글꼴을 유지했다. 프리팹은 아직 메인 씬에 배치하지 않았다.
- 보존한 반투명 프리팹 제목은 이순신 Bold, 본문은 이순신돋움 M의 정적 TMP 폰트다. 현재 문구와 조준 시 문구의 글자를 포함한다. 새 문구를 추가할 때는 폰트 글리프 포함 여부를 확인한다. 원본 TTF 5종과 라이선스 안내는 `../Fonts/YiSunShin/`에 있다.
- 기존 `DoorHoverFeedback`은 목재판의 조준 글자·문구 강조에 계속 사용한다. 테두리 강조 확장 기능은 보존했고 메인 문에서는 테두리 배열을 비웠다. 추후 프리팹을 배치할 때 같은 컴포넌트의 제목·본문·테두리 4개를 연결하면 된다. 패널에는 Collider가 없다.
- 임시 Editor 스크립트와 미확정 반대편 배치 이미지 등을 정리했다. 현재 메인 씬 배치는 `docs/entrance-route-guide/images/20261002-*-adjacent-door-flag.png`를 참고한다. `*-final-vr-ui.png`는 반투명 디자인·강조 기능의 이전 검증 기록이다.


검증: Unity 컴파일, 필요한 글리프 포함, 테두리 8개 연결, 강조/비활성화 복원 및 공유 재질 불변 검사, 실제 씬 기본·강조 렌더링, 씬 저장·재열기. 기존 경로 변경을 제외한 UI 작업의 기존 직렬화 블록 변경은 두 안내판 PrefabInstance와 두 DoorHoverFeedback뿐이다. XR Rig·Terrain·환경·문 이동 로직·게임 씬은 유지했다. 실제 HMD 가독성 및 트리거 씬 이동은 이번 작업에서 검증하지 않았다.
