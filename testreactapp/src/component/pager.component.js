import React from 'react';
import PropTypes from 'prop-types';

const propTypes = {
    items: PropTypes.array.isRequired,
    onChangePage: PropTypes.func.isRequired,
    initialPage: PropTypes.number,
    pageSize: PropTypes.number
}

const defaultProps = {
    initialPage: 1,
    pageSize: 2
}

class Pagination extends React.Component {
    constructor(props) {
        super(props);
        this.state = { pager: {} 
        };       
    }

    UNSAFE_componentWillMount() {
        if (this.props.items && this.props.items.length) {
            this.setPage(this.props.initialPage);          
        }
    }

    componentDidUpdate(prevProps, prevState) {       
        if (this.props.items !== prevProps.items) {
            this.setPage(this.props.initialPage);
        }
    }

    setPage(page) {
        var { items, pageSize } = this.props;
        var pager = this.state.pager;
       
        if (page < 0) {
           
            return;
        }
        
        pager = this.getPager(items.length, page, pageSize);
        var pageOfItems = items.slice(pager.startIndex, pager.endIndex + 1);     
        
        this.setState({ pager: pager });       
        this.props.onChangePage(pageOfItems);
    }

    getPager(totalItems, currentPage, pageSize) {       
        currentPage = currentPage || 1;     
        pageSize = pageSize || 10;
       
        var totalPages = Math.ceil(totalItems / pageSize);
        var showPages = 3;
        var startPage, endPage;
        
        if (totalPages <= showPages) {            
            startPage = 1;
            endPage = totalPages;
        } else {           
            if (currentPage <= 2) {
                startPage = 1;
                endPage = showPages;
            } else if (currentPage + 1 >= totalPages) {
                startPage = totalPages - showPages + 1;
                endPage = totalPages;
            } else {
                startPage = currentPage - 1;
                endPage = currentPage + 1;
            }        
        }

        var startIndex = (currentPage - 1) * pageSize;
        var endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);
       
        var pages = [...Array((endPage + 1) - startPage).keys()].map(i => startPage + i);

        return {
            totalItems: totalItems,
            currentPage: currentPage,
            pageSize: pageSize,
            totalPages: totalPages,
            startPage: startPage,
            endPage: endPage,
            startIndex: startIndex,
            endIndex: endIndex,
            pages: pages
        };
    }

    render() {
        var pager = this.state.pager;
        const { withTranslation } = this.props

        if (!pager.pages || pager.pages.length <= 1) {          
            return null;
        }
    
        return (
            <nav aria-label="...">
                <ul className="pagination justify-content-center">
                    <li className={pager.currentPage === 1 ? 'page-item disabled' : ''}>
                        <a className="page-link" onClick={() => this.setPage(1)}>{ withTranslation('pager.first') }</a>                   
                    </li>
                    <li className={pager.currentPage === 1 ? 'page-item disabled' : ''}>
                        <a className="page-link" onClick={() => this.setPage(pager.currentPage - 1)}>{ withTranslation('pager.prev') }</a>                    
                    </li>
                    {pager.pages.map((page, index) =>
                        <li key={index} className={pager.currentPage === page ? 'page-item active' : ''}>
                            <a className="page-link" onClick={() => this.setPage(page)}>{page}</a>                      
                        </li>
                    )}                   
                    <li className={pager.currentPage === pager.totalPages ? 'page-item disabled' : ''}>
                        <a className="page-link" onClick={() => this.setPage(pager.currentPage + 1)}>{ withTranslation('pager.next') }</a>                   
                    </li>
                    <li className={pager.currentPage === pager.totalPages ? 'page-item disabled' : ''}>
                        <a className="page-link" onClick={() => this.setPage(pager.totalPages)}>{ withTranslation('pager.last') }</a>                    
                    </li>
                </ul>
            </nav>
        );
    }
};

Pagination.propTypes = propTypes;
Pagination.defaultProps = defaultProps;
export default Pagination;