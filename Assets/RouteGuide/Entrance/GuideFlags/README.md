# 세로 깃발 및 VR 문 안내

2026-10-02. 적용 씬: `Assets/Scenes/main_playoursound.unity`.

- 기존 활성 경로 깃발 11개: 체험장 9개, 사방치기 2개. `Experience.png`, `Sabangchigi.png`와 `Guide_*.mat`를 사용한다.
- 문 안내 옆 새 깃발: 장구·사방치기 각 1개. 따뜻한 미색 바탕과 청록·주홍·황토 장식, 이순신 Bold 세로 글자. `Warm_Janggu.png`, `Warm_Sabangchigi.png` 및 해당 재질을 사용한다. 리본은 글자를 가리지 않도록 천 바깥으로 옮겼다. 복제본 Collider는 비활성이다.
- `Entrance_Guide_DoubleSided.asset`: 원본 제비꼬리 메시 복제. 위치·노멀·삼각형을 유지하고 +Z 면 UV의 U만 `1-U`로 보정했다. 원본 메시와 공유 천 재질은 변경하지 않았다.
- 두 문 UI: 기존 목재 면·기둥을 비활성화하고 `FloatingPanel`을 추가했다. `DoorPanel_Glass`, `DoorPanel_TextBackdrop`, `DoorPanel_Trim`은 URP/Lit이다. 패널은 배경이 비치는 청록색, 글자 뒤에는 진한 바탕을 사용한다. 담장과의 겹침을 피하도록 글자와 패널을 함께 앞으로 보정했다.
- 제목은 이순신 Bold, 본문은 이순신돋움 M의 정적 TMP 폰트다. 현재 문구와 조준 시 문구의 글자를 포함한다. 새 문구를 추가할 때는 폰트 글리프 포함 여부를 확인한다. 원본 TTF 5종과 라이선스 안내는 `../Fonts/YiSunShin/`에 있다.
- 기존 `DoorHoverFeedback`에서 문 조준 시 테두리·제목·문구가 금색으로 강조되고, 해제 또는 비활성화 시 복원된다. MaterialPropertyBlock을 사용하여 공유 재질은 바꾸지 않는다. 문 이동 로직·컨트롤러 참조는 유지했다. 패널에는 Collider가 없다.
- 배치·비교용 Editor 스크립트, 임시 씬과 미사용 시안은 정리했다. 최종 검증 이미지 두 개만 `docs/entrance-route-guide/images/20261002-*-final-vr-ui.png`에 보존한다. 왼쪽 기본 상태, 오른쪽 조준 강조 상태다.

검증: Unity 컴파일, 필요한 글리프 포함, 테두리 8개 연결, 강조/비활성화 복원 및 공유 재질 불변 검사, 실제 씬 기본·강조 렌더링, 씬 저장·재열기. 기존 경로 변경을 제외한 UI 작업의 기존 직렬화 블록 변경은 두 안내판 PrefabInstance와 두 DoorHoverFeedback뿐이다. XR Rig·Terrain·환경·문 이동 로직·게임 씬은 유지했다. 실제 HMD 가독성 및 트리거 씬 이동은 이번 작업에서 검증하지 않았다.
