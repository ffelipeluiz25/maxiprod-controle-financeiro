import React from 'react';
import styles from './Sidebar.module.css';

interface MenuItem {
    id: string;
    label: string;
    icon?: string;
    path: string;
}

interface SidebarProps {
    menuItems: MenuItem[];
    activePath: string;
    onNavigate: (path: string) => void;
}

export const Sidebar: React.FC<SidebarProps> = ({ menuItems, activePath, onNavigate }) => {
    return (
        <aside className={styles.sidebar}>
            <div className={styles.logo}>
                <h2>Finance Control</h2>
            </div>
            <nav className={styles.nav}>
                {menuItems.map((item) => (
                    <button
                        key={item.id}
                        className={`${styles.navItem} ${activePath === item.path ? styles.active : ''}`}
                        onClick={() => onNavigate(item.path)}
                    >
                        {item.icon && <span className={styles.icon}>{item.icon}</span>}
                        <span>{item.label}</span>
                    </button>
                ))}
            </nav>
        </aside>
    );
};