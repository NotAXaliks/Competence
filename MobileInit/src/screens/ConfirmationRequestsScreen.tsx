import React, { useEffect, useState } from 'react';
import {
    View,
    Text,
    StyleSheet,
    FlatList,
    TouchableOpacity,
} from 'react-native';

export default function ConfirmationRequestsScreen() {
    const [requests, setRequests] = useState<any>([]);

    useEffect(() => {
        const updateRequests = async () => {
            setRequests([
                {
                    id: '1',
                    user: 'Алексей Смирнов',
                    skill: 'Python',
                    level: '9/10',
                    time: '10 мин назад',
                },
                {
                    id: '2',
                    user: 'Мария Иванова',
                    skill: 'Docker',
                    level: '7/10',
                    time: '30 мин назад',
                },
            ]);
        };

        updateRequests();

        const intervalId = setInterval(updateRequests, 30_000);

        return () => {
            clearInterval(intervalId);
        };
    }, []);

    return (
        <View style={styles.container}>
            <Text style={styles.header}>Запросы</Text>
            <FlatList
                data={requests}
                keyExtractor={(item) => item.id}
                renderItem={({ item }) => (
                    <View style={styles.card}>
                        <View>
                            <Text style={styles.user}>{item.user}</Text>
                            <Text>
                                Запросил {item.skill} {item.level}
                            </Text>
                            <Text>{item.time}</Text>
                        </View>
                        <View>
                            <TouchableOpacity style={styles.acceptBtn}>
                                <Text style={styles.btnText}>✓</Text>
                            </TouchableOpacity>
                            <TouchableOpacity style={styles.rejectBtn}>
                                <Text style={styles.btnText}>✕</Text>
                            </TouchableOpacity>
                        </View>
                    </View>
                )}
            />
        </View>
    );
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },
    header: {
        fontSize: 30,
        fontWeight: '700',
        marginTop: 30,
        marginBottom: 20,
    },
    card: {
        backgroundColor: 'gray',
        padding: 18,
        marginBottom: 14,
        flexDirection: 'row',
        justifyContent: 'space-between',
    },
    user: {
        color: 'white',
        fontSize: 18,
        fontWeight: '700',
    },
    acceptBtn: {
        width: 44,
        height: 44,
        borderRadius: 12,
        backgroundColor: 'green',
        justifyContent: 'center',
        alignItems: 'center',
        marginBottom: 10,
    },
    rejectBtn: {
        width: 44,
        height: 44,
        borderRadius: 12,
        backgroundColor: 'red',
        justifyContent: 'center',
        alignItems: 'center',
    },
    btnText: {
        color: 'white',
        fontWeight: '700',
        fontSize: 18,
    },
});