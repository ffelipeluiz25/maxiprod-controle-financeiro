import React from 'react';
import styles from './DataTable.module.css';

interface Column<T> {
    key: keyof T;
    header: string;
    render?: (value: any, item: T) => React.ReactNode;
}

interface DataTableProps<T> {
    columns: Column<T>[];
    data: T[];
    onEdit?: (item: T) => void;
    onDelete?: (item: T) => void;
    actions?: boolean;
}

export function DataTable<T extends { id: number }>({
    columns,
    data,
    onEdit,
    onDelete,
    actions = true,
}: DataTableProps<T>) {
    return (
        <div className={styles.tableContainer}>
            <table className={styles.table}>
                <thead>
                    <tr>
                        {columns.map((col) => (
                            <th key={String(col.key)}>{col.header}</th>
                        ))}
                        {actions && (onEdit || onDelete) && <th>Ações</th>}
                    </tr>
                </thead>
                <tbody>
                    {data.map((item) => (
                        <tr key={item.id}>
                            {columns.map((col) => (
                                <td key={String(col.key)}>
                                    {col.render ? col.render(item[col.key], item) : String(item[col.key])}
                                </td>
                            ))}
                            {(onEdit || onDelete) && (
                                <td className={styles.actions}>
                                    {onEdit && (
                                        <button onClick={() => onEdit(item)} className={styles.editBtn}>
                                            Editar
                                        </button>
                                    )}
                                    {onDelete && (
                                        <button onClick={() => onDelete(item)} className={styles.deleteBtn}>
                                            Excluir
                                        </button>
                                    )}
                                </td>
                            )}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}