import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  Modal,
  ToastAndroid,
} from 'react-native';
import { Button, TextInput } from 'react-native-paper';

export default function PinScreen({ setScreen }: any) {
  const [pin, setPin] = useState('');
  const [modalVisible, setModalVisible] = useState(false);

  const addNumber = async (num: string) => {
    const NEEDS_PIN = "0123";
    const newPin = pin.slice(-3) + num;
    setPin(newPin);

    if (newPin === NEEDS_PIN) {
      // Пытаемся узнать, работает ли сеть
      try {
        await fetch("http://localhost:5299");

        setScreen('main')
      } catch (error) {
        console.error(error);
        ToastAndroid.show('Не могу подключиться к API. Проверьте соединение', ToastAndroid.SHORT);
      }
    } else if (newPin.length === NEEDS_PIN.length) {
      ToastAndroid.show('Неверный пин код', ToastAndroid.SHORT);
    }
  };

  const handleClose = () => {
    setModalVisible(false);

    ToastAndroid.show('Успешно', ToastAndroid.SHORT);
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Введите PIN</Text>

      <Text style={[styles.title, { margin: 10 }]}>{pin.split("").map(() => ".")}</Text>

      <View style={styles.keyboard}>
        {[[1, 2, 3], [4, 5, 6], [7, 8, 9], [0]].map((nums, i) => (
          <View key={i} style={{ flexDirection: 'row' }}>
            {nums.map((num) => (
              <TouchableOpacity
                key={num}
                style={styles.key}
                onPress={() => addNumber(`${num}`)}
              >
                <Text style={styles.keyText}>{num}</Text>
              </TouchableOpacity>
            ))}
          </View>
        ))}

        <TouchableOpacity style={styles.key} onPress={() => setPin(pin.slice(0, -1))}>
          <Text style={styles.keyText}>⌫</Text>
        </TouchableOpacity>
      </View>

      <TouchableOpacity onPress={() => setModalVisible(true)}>
        <Text>Забыли пароль?</Text>
      </TouchableOpacity>

      <Modal visible={modalVisible} transparent={true}>
        <View style={styles.modalOverlay}>
          <View style={styles.modalContent}>
            <TextInput 
              placeholder="Введите почту" 
              style={styles.input} 
            />

            <Button onPress={handleClose}>Закрыть</Button>
          </View>
        </View>
      </Modal>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  title: {
    fontSize: 28,
  },
  keyboard: {
    alignItems: 'center',
  },
  key: {
    width: 80,
    height: 80,
    borderRadius: "50%",
    backgroundColor: 'black',
    justifyContent: 'center',
    alignItems: 'center',
    margin: 5,
  },
  keyText: {
    color: 'white',
    fontSize: 20,
  },
  modalOverlay: { flex: 1, backgroundColor: 'rgba(0,0,0,0.5)', justifyContent: 'center', alignItems: 'center' },
  modalContent: { width: "80%", backgroundColor: 'white', padding: 20, borderRadius: 10, alignItems: 'center' },
  input: { width: '100%' },
});
