import React from 'react';
import {
    Text,
    StyleSheet,
    ScrollView,
    Linking,
} from 'react-native';
import { Button, Divider } from 'react-native-paper';

const skills = [
    { name: 'React', level: 9, confirmations: 12 },
    { name: 'TypeScript', level: 8, confirmations: 10 },
    { name: 'Node.js', level: 8, confirmations: 9 },
    { name: 'Docker', level: 6, confirmations: 4 },
    { name: 'PostgreSQL', level: 7, confirmations: 5 },
];
const confirmations = [
    { name: "Петров", what: "React", date: new Date() },
    { name: "Сидорова", what: "React", date: new Date() },
];

export default function ProfileScreen({}: any) {
    return (
        <ScrollView style={styles.container}>
            <Text>Топ навыков</Text>
            {skills.map((skill) => (
                <Text>{`${skill.name} ${skill.level}/10 ${"✓".repeat(skill.confirmations)}`}</Text>
            ))}
            <Divider style={{ marginVertical: 10 }}  />
            <Text>Подтверждения</Text>
            {confirmations.slice(-3).map((confirmation) => (
                <Text>{`${confirmation.name}: ${confirmation.what} [${confirmation.date.toLocaleDateString()}]`}</Text>
            ))}
            <Divider style={{ marginVertical: 10 }}  />

            <Button onPress={() => Linking.openURL("http://google.com")}>Редактировать в Web</Button>
            <Button>Экспорт PDF</Button>
        </ScrollView>
    );
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },
});