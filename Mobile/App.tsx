import { useState } from 'react';
import { StyleSheet, TouchableOpacity, View } from 'react-native';
import { Text } from 'react-native-paper';
import DashboardScreen from './screens/DashboardScreen';
import ProfileScreen from './screens/ProfileScreen';
import PinScreen from './screens/PinScreen';
import NotificationsScreen from './screens/NotificationsScreen';

const pages = {
  main: {
    screen: (setScreen: any) => <DashboardScreen setScreen={setScreen} />,
    name: "🏠 Главная",
  },
  profile: {
    screen: (setScreen: any) => <ProfileScreen setScreen={setScreen} />,
    name: "👤 Профиль",
  },
  notifications: {
    screen: (setScreen: any) => <NotificationsScreen setScreen={setScreen} />,
    name: "🔔 Уведомления",
  },
  export: {
    screen: (setScreen: any) => <DashboardScreen setScreen={setScreen} />,
    name: "📄 Экспорт",
  },
}

function App() {
  const [currentScreen, setScreen] = useState("login");

  if (currentScreen === 'login') {
    return (
      <View style={styles.container}>
        <PinScreen setScreen={setScreen} />
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <View style={{ justifyContent: 'center', margin: 10 }}>
        <Text style={{ fontSize: 20, fontWeight: 'bold' }}>ИТ-Проф</Text>
      </View>

      <View style={styles.container}>
        {pages[currentScreen].screen(setScreen)}
      </View>

      <View style={styles.tabBar}>
        {
          Object.entries(pages).map(([key, { name }]) => {
            return (
              <TouchableOpacity key={key} style={[styles.tabButton, currentScreen === key && { backgroundColor: 'blue' }]} onPress={() => setScreen(key)}>
                <Text>{name}</Text>
              </TouchableOpacity>
            )
          })
        }
      </View>
    </View>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5F5F5',
  },
  tabBar: {
    height: 60,
    flexDirection: 'row',
    backgroundColor: '#ffffff',
  },
  tabButton: {
    flex: 1,
    justifyContent: 'center',  // Центр по вертикали
    alignItems: 'center',      // Центр по горизонтали
  },
});

export default App;
