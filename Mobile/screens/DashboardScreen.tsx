import React from 'react';
import {
    View,
    Text,
    StyleSheet,
    ScrollView,
    TouchableOpacity,
} from 'react-native';
import { ProgressBar } from 'react-native-paper';

const notifications = [
    'Новый запрос подтверждения Python',
    'Рейтинг вырос на 3%',
    'Новая вакансия Frontend Developer',
    'Подтверждение принято',
    'Обновите профиль',
];

export default function DashboardScreen({}: any) {
    return (
        <View style={styles.container}>
            <ScrollView>
                <View style={styles.progressCard}>
                    <Text>Индекс компетенций 85%</Text>
                    <ProgressBar 
                        progress={0.85} 
                        color="#4F46E5"
                        style={styles.progressBar} 
                    />
                    <Text>Подтверждений: 15 (+2 новых)</Text>
                </View>
                <View style={styles.progressCard}>
                    <Text>Доверие 85%</Text>
                    <ProgressBar 
                        progress={0.85} 
                        color="#4F46E5"
                        style={styles.progressBar} 
                    />
                </View>

                <Text>Последние уведомления</Text>
                {notifications.length ? notifications.map((item, index) => (
                    <View key={index} style={styles.notificationCard}>
                        <Text style={{ color: 'white' }}>{item}</Text>
                    </View>
                )) : <Text>Нет новых уведомлений</Text>}
            </ScrollView>

            <TouchableOpacity style={styles.fab}>
                <Text style={{ color: "white" }}>+</Text>
            </TouchableOpacity>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },
    progressCard: {
        backgroundColor: 'white',
        padding: 10,
        marginBottom: 20,
    },
    progressBar: {
        height: 8,
        borderRadius: 4,
    },
    notificationCard: {
        backgroundColor: 'black',
        padding: 16,
        margin: 10,
    },
    fab: {
        position: 'absolute',
        right: 20,
        bottom: 24,
        width: 56,
        height: 56,
        borderRadius: 28,
        backgroundColor: 'blue',
        justifyContent: 'center',
        alignItems: 'center',
    },
});