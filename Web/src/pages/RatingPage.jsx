import React, { useState, useEffect } from 'react';
import { Card, Typography, Statistic, Spin } from 'antd';
import { PieChart, Pie, Cell, ResponsiveContainer, Tooltip, Legend } from 'recharts';
import { ApiService } from '../services/ApiService';

export default function RatingSummary() {
    const [rating, setRating] = useState(null);

    const pieData = [
        { name: 'Образование', value: 30, color: '#1890ff' },
        { name: 'Опыт работы', value: 30, color: '#52c41a' },
        { name: 'Подтверждения', value: 40, color: '#faad14' },
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
                        {pieData.map((entry, index) => (
                            <Cell key={index} fill={entry.color} />
                        ))}
                    </Pie>
                    <Tooltip />
                    <Legend verticalAlign="bottom" height={36} />
                </PieChart>
            </ResponsiveContainer>
        </Card>
    );
}