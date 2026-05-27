import { useState } from 'react';
import { StyleSheet, TouchableOpacity, View } from 'react-native';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import { Text } from 'react-native-paper';
import MainScreen from './screens/MainScreen';
import SecondScreen from './screens/SecondScreen';

function App() {
  const [currentScreen, setScreen] = useState("main");

  const renderScreen = () => {
    switch (currentScreen) {
      case "main":
        return <MainScreen />;
      default:
        return <SecondScreen />;
    }
  }

  return (
    <SafeAreaProvider style={styles.container}>
      <View style={styles.container}>
        {renderScreen()}
      </View>

      <View style={styles.tabBar}>
        <TouchableOpacity style={styles.tabButton} onPress={() => setScreen("main")}>
          <Text>Главная</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.tabButton} onPress={() => setScreen("second")}>
          <Text>Вторая</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.tabButton} onPress={() => setScreen("second")}>
          <Text>Главная</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaProvider>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  tabBar: {
    height: 60,
    flexDirection: 'row',
    backgroundColor: '#ffffff',
  },
  tabButton: {
    flex: 1,                   // Каждая кнопка растянется на равную долю ширины
    justifyContent: 'center',  // Центрируем текст по вертикали
    alignItems: 'center',      // Центрируем текст по горизонтали
  },
});

export default App;
