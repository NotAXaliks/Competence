import React from 'react';
import {
    View,
    Text,
    StyleSheet,
    FlatList,
    TouchableOpacity,
} from 'react-native';
import { GestureHandlerRootView, Swipeable } from 'react-native-gesture-handler';

const notifications = [
    {
        id: '1',
        title: 'Новый запрос подтверждения',
        time: '5 мин назад',
    },
    {
        id: '2',
        title: 'Ваш рейтинг вырос до 91%',
        time: '1 час назад',
    },
    {
        id: '3',
        title: 'Совпадение с вакансией Frontend Dev',
        time: '3 часа назад',
    },
];

export default function NotificationsScreen() {
    const renderRightActions = (id: string) => {
        return (
        <TouchableOpacity
        >
            <Text>{id}</Text>
        </TouchableOpacity>
        );
    };

    return (
        <GestureHandlerRootView style={styles.container}>
            <Text style={styles.header}>Уведомления</Text>
            <FlatList
                data={notifications}
                keyExtractor={(item) => item.id}
                renderItem={({ item }) => (
                    <Swipeable renderRightActions={() => renderRightActions(item.id)}>
                        <View style={styles.card}>
                            <Text style={styles.title}>{item.title}</Text>
                            <Text>{item.time}</Text>
                        </View>
                    </Swipeable>
                )}
            />
        </GestureHandlerRootView>
    );
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },
    header: {
        fontSize: 30,
        fontWeight: 'bold',
        marginBottom: 20,
    },
    card: {
        backgroundColor: 'gray',
        padding: 18,
        marginBottom: 14,
    },
    title: {
        color: 'white',
        fontSize: 17,
        fontWeight: 'bold',
    },
});
