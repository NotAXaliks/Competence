## Установка Mobile:

1. `npx @react-native-community/cli init Mobile --version 0.82.0`. Обязательно 0.82.0!
2. создать файл в android/local.properties с путем до Android SDK
```
sdk.dir=C:\\Users\\Xaliks\\AppData\\Local\\Android\\Sdk
```
3. `adb reverse tcp:API_PORT tcp:API_PORT`
