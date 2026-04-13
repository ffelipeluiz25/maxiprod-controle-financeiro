import React, { useEffect } from 'react';
import styles from './Modal.module.css';

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    title: string;
    children: React.ReactNode;
    closeOnBackdropClick?: boolean; 
    closeOnEsc?: boolean; 
}

export const Modal: React.FC<ModalProps> = ({ isOpen, onClose, 
    title, children, closeOnBackdropClick = true, closeOnEsc = true }) => {
    useEffect(() => {
        const handleEsc = (e: KeyboardEvent) => {
            if (e.key === 'Escape') onClose();
        };
        
        if (isOpen) {
            document.addEventListener('keydown', handleEsc);
            document.body.style.overflow = 'hidden';
        }
        return () => {
            document.removeEventListener('keydown', handleEsc);
            document.body.style.overflow = 'unset';
        };
    }, [isOpen, onClose]);

    const handleBackdropClick = (e: React.MouseEvent) => {
        if (closeOnBackdropClick) {
          onClose();
        }
      };
      
    if (!isOpen) return null;

    return (
        <div className={styles.overlay} onClick={handleBackdropClick}>
          <div className={styles.modal} onClick={(e) => e.stopPropagation()}>
            <div className={styles.header}>
              <h2>{title}</h2>
              <button className={styles.closeBtn} onClick={onClose}>
                ×
              </button>
            </div>
            <div className={styles.content}>{children}</div>
          </div>
        </div>
      );
};