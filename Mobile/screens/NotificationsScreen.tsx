import React from 'react';
import {
    Text,
    StyleSheet,
    ScrollView,
    View,
} from 'react-native';
import { Button, Divider } from 'react-native-paper';

const notifications = [
    { type: "profile", title: "Петров запросил подтверждение Python 1/10", timestamp: new Date() },
    { type: "profile", title: "Рейтинг вырос на 3%", timestamp: new Date() },
    { type: "profile", title: "Совпадение с вакансией Frontend Dev", timestamp: new Date() },
];

export default function NotificationsScreen({ setScreen }: any) {
    return (
        <ScrollView style={styles.container}>
            {notifications.map(({ type, title, timestamp }, index) => (
                <View key={index}>
                    <Text style={{ fontWeight: "bold", fontSize: 20 }}>{title}</Text>
                    <Text>{timestamp.toLocaleDateString()}</Text>
                    <Button onPress={() => setScreen(type)}>Подробнее</Button>

                    <Divider style={{ marginVertical: 10 }} />
                </View>
            ))}
        </ScrollView>
    );
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },
});