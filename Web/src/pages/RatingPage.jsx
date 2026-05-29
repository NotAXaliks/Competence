import React, { useState, useEffect } from 'react';
import { Card, Typography, Statistic, Spin } from 'antd';
import { PieChart, Pie, Cell, ResponsiveContainer, Tooltip, Legend } from 'recharts';
import { ApiService } from '../services/ApiService';

export default function RatingSummary() {
    const [rating, setRating] = useState(null);

    const pieData = [
        { name: 'Образование', value: 30 },
        { name: 'Опыт работы', value: 30 },
        { name: 'Подтверждения', value: 40 },
    ];

    useEffect(() => {
        const fetchData = async () => {
            const res = await ApiService.request("GET", "/users/me/rating");

            if (res && res.Data) setRating(res.Data);
        };
        fetchData();
    }, []);

    return (
        <Card title="Мой рейтинг">
            <Statistic
                title="Текущий индекс компетенций"
                value={rating?.index || 0}
                suffix="%"
            />

            <ResponsiveContainer height="300">
                <PieChart>
                    <Pie data={pieData}>
                        <Cell fill={"#1890ff"} />
                        <Cell fill={"#52c41a"} />
                        <Cell fill={"#faad14"} />
                    </Pie>
                    <Tooltip />
                    <Legend verticalAlign="bottom" />
                </PieChart>
            </ResponsiveContainer>
        </Card>
    );
}